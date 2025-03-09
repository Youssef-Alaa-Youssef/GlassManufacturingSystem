using Factory.BLL.InterFaces;
using Factory.DAL.Models.Auth;
using Factory.DAL.Models.OrderList;
using Factory.DAL.Models.Settings;
using Factory.PL.Services.DataExportImport;
using Factory.PL.ViewModels.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Dynamic;
using Microsoft.Extensions.Caching.Memory;

namespace Factory.PL.Controllers
{

    public class ExportImportController : Controller
    {
        private readonly IExportService _exportService;
        private readonly IImportService _importService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ExportImportController> _logger;
        private readonly IMemoryCache _memoryCache;

        public ExportImportController(
            IExportService exportService,
            IImportService importService,
            IUnitOfWork unitOfWork,
            ILogger<ExportImportController> logger,
            IMemoryCache memoryCache)
        {
            _exportService = exportService ?? throw new ArgumentNullException(nameof(exportService));
            _importService = importService ?? throw new ArgumentNullException(nameof(importService));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
        }


        [HttpGet]
        public async Task<IActionResult> Settings()
        {
            var existingSettings = await _unitOfWork.GetRepository<ExportImportSettings>().GetFirstOrDefaultAsync();
            return View(existingSettings ?? new ExportImportSettings());
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Settings(ExportImportSettings model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var existingSettings = await _unitOfWork.GetRepository<ExportImportSettings>().GetFirstOrDefaultAsync();

            if (existingSettings == null)
            {
                await _unitOfWork.GetRepository<ExportImportSettings>().AddAsync(model);
            }
            else
            {
                UpdateExistingSettings(existingSettings, model);
                await _unitOfWork.GetRepository<ExportImportSettings>().UpdateAsync(existingSettings);
            }

            await _unitOfWork.SaveChangesAsync();

            const string cacheKey = "ExportImportSettings";
            _memoryCache.Remove(cacheKey);

            TempData["Success"] = "Settings have been saved successfully.";

            return RedirectToAction(nameof(Settings));
        }

        [HttpGet]
        public async Task<IActionResult> Export()
        {
            var existingSettings = await GetExportSettingsAsync();
            if (existingSettings == null || !existingSettings.EnableExport)
            {
                TempData["Error"] = existingSettings == null ? "Export settings not found." : "Export functionality is disabled.";
                return RedirectToAction(nameof(Settings));
            }

            var viewModel = new ExportViewModel
            {
                SupportedFormats = existingSettings.SupportedExportFormats,
                SelectedFormat = existingSettings.DefaultExportFormat
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> ExportData<T>(string format, IEnumerable<T> data, List<string> selectedColumns)
        {
            var existingSettings = await GetExportSettingsAsync();
            if (existingSettings == null || !existingSettings.EnableExport)
            {
                TempData["Error"] = existingSettings == null ? "Export settings are not configured." : "Export functionality is disabled.";
                return RedirectToAction(nameof(Settings));
            }

            if (string.IsNullOrWhiteSpace(format))
            {
                format = existingSettings.DefaultExportFormat;
            }

            if (!_exportService.IsFormatSupported(format, existingSettings))
            {
                TempData["Error"] = $"Export format '{format}' is not supported.";
                return RedirectToAction(nameof(Settings));
            }

            if (data == null || !data.Any())
            {
                TempData["Error"] = "No data available to export.";
                return RedirectToAction(nameof(Settings));
            }

            try
            {
                var filteredData = data.Select(item =>
                {
                    var filteredItem = new ExpandoObject() as IDictionary<string, object>;
                    foreach (var column in selectedColumns)
                    {
                        var property = item.GetType().GetProperty(column);
                        if (property != null)
                        {
                            filteredItem[column] = property.GetValue(item);
                        }
                    }
                    return filteredItem;
                });

                var result = await _exportService.ExportDataAsync(filteredData, format, existingSettings);
                string contentType = _exportService.GetContentType(format);
                string fileExtension = _exportService.GetFileExtension(format);
                string fileName = $"Export_{DateTime.Now:yyyyMMdd_HHmmss}{fileExtension}";

                return File(result, contentType, fileName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error exporting data.");
                TempData["Error"] = "An error occurred while exporting data.";
                return RedirectToAction(nameof(Settings));
            }
        }
        [HttpGet]
        public async Task<IActionResult> Import()
        {
            var existingSettings = await GetExportSettingsAsync();
            if (existingSettings == null || !existingSettings.EnableImport)
            {
                TempData["Error"] = existingSettings == null ? "Import settings not found." : "Import functionality is disabled.";
                return RedirectToAction(nameof(Settings));
            }

            var viewModel = new ImportViewModel
            {
                SupportedFormats = existingSettings.SupportedImportFormats
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> ImportData<T>(IFormFile file, string format) where T : new()
        {
            var existingSettings = await GetExportSettingsAsync();
            if (existingSettings == null || !existingSettings.EnableImport)
            {
                return BadRequest(existingSettings == null ? "Import settings are not configured." : "Import functionality is disabled.");
            }

            if (file == null || file.Length == 0)
            {
                return BadRequest("No file was uploaded.");
            }

            format = DetermineFileFormat(file, format);
            if (format == null)
            {
                return BadRequest("Could not determine file format. Please specify a format.");
            }

            if (!_importService.IsFormatSupported(format, existingSettings))
            {
                return BadRequest($"Import format '{format}' is not supported.");
            }

            if (!_importService.ValidateFileSize(file.Length, existingSettings))
            {
                return BadRequest($"File size exceeds the maximum allowed size of {existingSettings.MaxImportFileSize / (1024 * 1024)}MB.");
            }

            try
            {
                using var stream = file.OpenReadStream();
                var importedData = await _importService.ImportDataAsync<T>(stream, format, existingSettings);

                return Ok($"Successfully imported {importedData.Count()} items.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error importing data.");
                return StatusCode(500, $"Error importing data: {ex.Message}");
            }
        }
        
        [HttpPost]
        public async Task<IActionResult> GenericExport(string modelType, string format, List<string> selectedColumns)
        {
            var existingSettings = await GetExportSettingsAsync();
            if (existingSettings == null || !existingSettings.EnableExport)
            {
                return BadRequest(existingSettings == null ? "Export settings are not configured." : "Export functionality is disabled.");
            }

            if (string.IsNullOrWhiteSpace(modelType))
            {
                return BadRequest("No data type selected.");
            }

            if (selectedColumns == null || !selectedColumns.Any())
            {
                return BadRequest("No columns selected.");
            }

            if (string.IsNullOrWhiteSpace(format))
            {
                format = existingSettings.DefaultExportFormat;
            }

            if (!_exportService.IsFormatSupported(format, existingSettings))
            {
                return BadRequest($"Export format '{format}' is not supported.");
            }

            try
            {
                var data = await GetDataByModelTypeAsync(modelType, selectedColumns);
                var result = await _exportService.ExportDataAsync(data, format, existingSettings);
                string contentType = _exportService.GetContentType(format);
                string fileExtension = _exportService.GetFileExtension(format);
                string fileName = $"{modelType}_{DateTime.Now:yyyyMMdd_HHmmss}{fileExtension}";

                return File(result, contentType, fileName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error exporting data.");
                return StatusCode(500, $"Error exporting data: {ex.Message}");
            }
        }
        
        [HttpGet]
        public IActionResult GetColumns(string modelType)
        {
            var columns = modelType switch
            {
                "Users" => typeof(ApplicationUser).GetProperties().Select(p => p.Name).ToList(),
                "Roles" => typeof(IdentityRole).GetProperties().Select(p => p.Name).ToList(),
                "Orders" => typeof(Order).GetProperties().Select(p => p.Name).ToList(),
                _ => new List<string>()
            };

            return  Json(columns);
        }

        private async Task<ExportImportSettings?> GetExportSettingsAsync()
        {
            const string cacheKey = "ExportImportSettings";
            if (_memoryCache.TryGetValue(cacheKey, out ExportImportSettings? cachedSettings))
            {
                return cachedSettings;
            }
            var settings = await _unitOfWork.GetRepository<ExportImportSettings>().GetFirstOrDefaultAsync();
            var cacheOptions = new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromDays(360));
            _memoryCache.Set(cacheKey, settings, cacheOptions);
            return settings;
        }
        private void UpdateExistingSettings(ExportImportSettings existingSettings, ExportImportSettings newSettings)
        {
            existingSettings.EnableExport = newSettings.EnableExport;
            existingSettings.EnableImport = newSettings.EnableImport;
            existingSettings.SupportedExportFormats = newSettings.SupportedExportFormats;
            existingSettings.SupportedImportFormats = newSettings.SupportedImportFormats;
            existingSettings.MaxExportRows = newSettings.MaxExportRows;
            existingSettings.MaxImportFileSize = newSettings.MaxImportFileSize;
            existingSettings.IncludeHeaders = newSettings.IncludeHeaders;
            existingSettings.DateFormat = newSettings.DateFormat;
            existingSettings.DefaultExportFormat = newSettings.DefaultExportFormat;
            existingSettings.CsvDelimiter = newSettings.CsvDelimiter;
            existingSettings.AllowNullValues = newSettings.AllowNullValues;
            existingSettings.ValidateImportData = newSettings.ValidateImportData;
        }

        private string DetermineFileFormat(IFormFile file, string format)
        {
            if (!string.IsNullOrEmpty(format))
            {
                return format;
            }

            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            return extension switch
            {
                ".csv" => "CSV",
                ".json" => "JSON",
                ".xml" => "XML",
                ".xlsx" => "XLSX",
                _ => null
            };
        }
        private async Task<IEnumerable<object>> GetDataByModelTypeAsync(string modelType, List<string> selectedColumns)
        {
            List<object> data;

            switch (modelType)
            {
                case "Users":
                    data = (await _unitOfWork.GetRepository<ApplicationUser>().GetAllAsync()).Cast<object>().ToList();
                    break;
                case "Roles":
                    data = (await _unitOfWork.GetRepository<IdentityRole>().GetAllAsync()).Cast<object>().ToList();
                    break;
                case "Orders":
                    data = (await _unitOfWork.GetRepository<Order>().GetAllAsync()).Cast<object>().ToList();
                    break;
                default:
                    throw new ArgumentException($"Unsupported model type: {modelType}", nameof(modelType));
            }


            var filteredData = data.Select(item =>
            {
                var filteredItem = new ExpandoObject() as IDictionary<string, object>;
                foreach (var column in selectedColumns)
                {
                    var property = item.GetType().GetProperty(column);
                    if (property != null)
                    {
                        filteredItem[column] = property.GetValue(item);
                    }
                }
                return filteredItem;
            });

            return filteredData;
        }

    }
}