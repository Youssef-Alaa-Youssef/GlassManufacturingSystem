using System.Text;
using System.Xml.Serialization;
using Factory.DAL.Models.Settings;
using Newtonsoft.Json;
using OfficeOpenXml;

namespace Factory.PL.Services.DataExportImport
{
    public class ExportService : IExportService
    {
        private readonly ExportImportSettings _defaultSettings;

        public ExportService(ExportImportSettings defaultSettings = null)
        {
            _defaultSettings = defaultSettings ?? new ExportImportSettings();
        }

        public async Task<byte[]> ExportDataAsync<T>(IEnumerable<T> data, string format, ExportImportSettings settings = null)
        {
            settings = settings ?? _defaultSettings;

            if (!IsFormatSupported(format, settings))
            {
                throw new ArgumentException($"Format '{format}' is not supported");
            }

            format = format.ToUpper();

            return format switch
            {
                "CSV" => await ExportToCsvAsync(data, settings),
                "JSON" => await ExportToJsonAsync(data, settings),
                "XML" => await ExportToXmlAsync(data, settings),
                "XLSX" => await ExportToExcelAsync(data, settings),
                _ => throw new NotSupportedException($"Format '{format}' is not implemented")
            };
        }

        public string GetContentType(string format)
        {
            return format.ToUpper() switch
            {
                "CSV" => "text/csv",
                "JSON" => "application/json",
                "XML" => "application/xml",
                "XLSX" => "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                _ => "application/octet-stream"
            };
        }

        public string GetFileExtension(string format)
        {
            return format.ToUpper() switch
            {
                "CSV" => ".csv",
                "JSON" => ".json",
                "XML" => ".xml",
                "XLSX" => ".xlsx",
                _ => ".txt"
            };
        }

        public bool IsFormatSupported(string format, ExportImportSettings settings = null)
        {
            settings = settings ?? _defaultSettings;
            return settings.SupportedExportFormats.Contains(format.ToUpper());
        }

        private async Task<byte[]> ExportToCsvAsync<T>(IEnumerable<T> data, ExportImportSettings settings)
        {
            var properties = typeof(T).GetProperties()
                .Where(p => p.CanRead)
                .ToList();

            using var memoryStream = new MemoryStream();
            using var writer = new StreamWriter(memoryStream, Encoding.UTF8);

            // Write headers
            if (settings.IncludeHeaders)
            {
                var headerLine = string.Join(
                    settings.CsvDelimiter,
                    properties.Select(p => p.Name)
                );
                await writer.WriteLineAsync(headerLine);
            }

            // Write data rows
            foreach (var item in data.Take(settings.MaxExportRows))
            {
                var values = properties.Select(p =>
                {
                    var value = p.GetValue(item);
                    if (value == null)
                    {
                        return settings.AllowNullValues ? "" : "null";
                    }
                    else if (value is DateTime dateTime)
                    {
                        return dateTime.ToString(settings.DateFormat);
                    }
                    else
                    {
                        return value.ToString();
                    }
                });

                var line = string.Join(settings.CsvDelimiter, values);
                await writer.WriteLineAsync(line);
            }

            await writer.FlushAsync();
            return memoryStream.ToArray();
        }

        private async Task<byte[]> ExportToJsonAsync<T>(IEnumerable<T> data, ExportImportSettings settings)
        {
            var limitedData = data.Take(settings.MaxExportRows);
            var json = JsonConvert.SerializeObject(limitedData);

            return Encoding.UTF8.GetBytes(json);
        }

        private async Task<byte[]> ExportToXmlAsync<T>(IEnumerable<T> data, ExportImportSettings settings)
        {
            var limitedData = data.Take(settings.MaxExportRows).ToList();

            using var memoryStream = new MemoryStream();
            var serializer = new XmlSerializer(typeof(List<T>));
            serializer.Serialize(memoryStream, limitedData);

            return memoryStream.ToArray();
        }

        private async Task<byte[]> ExportToExcelAsync<T>(IEnumerable<T> data, ExportImportSettings settings)
        {
            var properties = typeof(T).GetProperties()
                .Where(p => p.CanRead)
                .ToList();

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using var package = new ExcelPackage();
            var worksheet = package.Workbook.Worksheets.Add("Exported Data");

            // Add headers
            if (settings.IncludeHeaders)
            {
                for (int i = 0; i < properties.Count; i++)
                {
                    worksheet.Cells[1, i + 1].Value = properties[i].Name;
                    worksheet.Cells[1, i + 1].Style.Font.Bold = true;
                }
            }

            // Add data rows
            var dataList = data.Take(settings.MaxExportRows).ToList();
            for (int row = 0; row < dataList.Count; row++)
            {
                var item = dataList[row];
                for (int col = 0; col < properties.Count; col++)
                {
                    var value = properties[col].GetValue(item);
                    if (value != null)
                    {
                        worksheet.Cells[row + (settings.IncludeHeaders ? 2 : 1), col + 1].Value = value;
                    }
                }
            }

            return await package.GetAsByteArrayAsync();
        }
    }
}