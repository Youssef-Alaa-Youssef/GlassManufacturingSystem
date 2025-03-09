using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using System.Text;
using System.Xml.Serialization;
using Factory.DAL.Models.Settings;
using Newtonsoft.Json;
using OfficeOpenXml;

namespace Factory.PL.Services.DataExportImport
{
    public class ImportService : IImportService
    {
        private readonly ExportImportSettings _defaultSettings;

        public ImportService(ExportImportSettings defaultSettings = null)
        {
            _defaultSettings = defaultSettings ?? new ExportImportSettings();
        }

        public async Task<IEnumerable<T>> ImportDataAsync<T>(Stream fileStream, string format, ExportImportSettings settings = null) where T : new()
        {
            settings = settings ?? _defaultSettings;

            if (!IsFormatSupported(format, settings))
            {
                throw new ArgumentException($"Format '{format}' is not supported");
            }

            if (fileStream.Length > settings.MaxImportFileSize)
            {
                throw new ArgumentException($"File size exceeds the maximum allowed size of {settings.MaxImportFileSize / (1024 * 1024)}MB");
            }

            format = format.ToUpper();

            return format switch
            {
                "CSV" => await ImportFromCsvAsync<T>(fileStream, settings),
                "JSON" => await ImportFromJsonAsync<T>(fileStream, settings),
                "XML" => await ImportFromXmlAsync<T>(fileStream, settings),
                "XLSX" => await ImportFromExcelAsync<T>(fileStream, settings),
                _ => throw new NotSupportedException($"Format '{format}' is not implemented")
            };
        }

        public bool IsFormatSupported(string format, ExportImportSettings settings = null)
        {
            settings = settings ?? _defaultSettings;
            return settings.SupportedImportFormats.Contains(format.ToUpper());
        }

        public bool ValidateFileSize(long fileSize, ExportImportSettings settings = null)
        {
            settings = settings ?? _defaultSettings;
            return fileSize <= settings.MaxImportFileSize;
        }

        private async Task<IEnumerable<T>> ImportFromCsvAsync<T>(Stream fileStream, ExportImportSettings settings) where T : new()
        {
            var result = new List<T>();
            using var reader = new StreamReader(fileStream, Encoding.UTF8);

            var properties = typeof(T).GetProperties()
                .Where(p => p.CanWrite)
                .ToList();

            // Read header line
            string headerLine = await reader.ReadLineAsync();
            if (string.IsNullOrEmpty(headerLine))
            {
                return result;
            }

            string[] headers = headerLine.Split(settings.CsvDelimiter);
            Dictionary<int, PropertyInfo> columnMap = new Dictionary<int, PropertyInfo>();

            // Map columns to properties
            for (int i = 0; i < headers.Length; i++)
            {
                var property = properties.FirstOrDefault(p =>
                    string.Equals(p.Name, headers[i], StringComparison.OrdinalIgnoreCase));

                if (property != null)
                {
                    columnMap[i] = property;
                }
            }

            // Read data rows
            string line;
            while ((line = await reader.ReadLineAsync()) != null)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }

                string[] values = line.Split(settings.CsvDelimiter);
                T item = new T();

                for (int i = 0; i < values.Length && i < headers.Length; i++)
                {
                    if (columnMap.TryGetValue(i, out PropertyInfo property))
                    {
                        SetPropertyValue(item, property, values[i], settings);
                    }
                }

                result.Add(item);
            }

            return result;
        }

        private async Task<IEnumerable<T>> ImportFromJsonAsync<T>(Stream fileStream, ExportImportSettings settings) where T : new()
        {
            using var reader = new StreamReader(fileStream, Encoding.UTF8);
            string json = await reader.ReadToEndAsync();

            return JsonConvert.DeserializeObject<IEnumerable<T>>(json);
        }

        private async Task<IEnumerable<T>> ImportFromXmlAsync<T>(Stream fileStream, ExportImportSettings settings) where T : new()
        {
            var serializer = new XmlSerializer(typeof(List<T>));
            return (List<T>)serializer.Deserialize(fileStream);
        }

        private async Task<IEnumerable<T>> ImportFromExcelAsync<T>(Stream fileStream, ExportImportSettings settings) where T : new()
        {
            var result = new List<T>();

            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            using var package = new ExcelPackage(fileStream);
            var worksheet = package.Workbook.Worksheets.First();

            var properties = typeof(T).GetProperties()
                .Where(p => p.CanWrite)
                .ToList();

            bool hasHeader = settings.IncludeHeaders;
            int startRow = hasHeader ? 2 : 1;

            Dictionary<int, PropertyInfo> columnMap = new Dictionary<int, PropertyInfo>();
            int colCount = worksheet.Dimension.End.Column;

            for (int col = 1; col <= colCount; col++)
            {
                string header = hasHeader ? worksheet.Cells[1, col].Text : $"Column{col}";

                var property = properties.FirstOrDefault(p =>
                    string.Equals(p.Name, header, StringComparison.OrdinalIgnoreCase));

                if (property != null)
                {
                    columnMap[col] = property;
                }
            }

            int rowCount = worksheet.Dimension.End.Row;
            for (int row = startRow; row <= rowCount; row++)
            {
                T item = new T();

                for (int col = 1; col <= colCount; col++)
                {
                    if (columnMap.TryGetValue(col, out PropertyInfo property))
                    {
                        string value = worksheet.Cells[row, col].Text;
                        SetPropertyValue(item, property, value, settings);
                    }
                }

                result.Add(item);
            }

            return result;
        }

        private void SetPropertyValue<T>(T item, PropertyInfo property, string value, ExportImportSettings settings)
        {
            if (string.IsNullOrEmpty(value) && !settings.AllowNullValues)
            {
                return;
            }

            try
            {
                object convertedValue;
                Type propType = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;

                if (propType == typeof(string))
                {
                    convertedValue = value;
                }
                else if (propType == typeof(int))
                {
                    convertedValue = int.Parse(value);
                }
                else if (propType == typeof(long))
                {
                    convertedValue = long.Parse(value);
                }
                else if (propType == typeof(double))
                {
                    convertedValue = double.Parse(value, CultureInfo.InvariantCulture);
                }
                else if (propType == typeof(decimal))
                {
                    convertedValue = decimal.Parse(value, CultureInfo.InvariantCulture);
                }
                else if (propType == typeof(bool))
                {
                    convertedValue = bool.Parse(value);
                }
                else if (propType == typeof(DateTime))
                {
                    convertedValue = DateTime.ParseExact(value, settings.DateFormat, CultureInfo.InvariantCulture);
                }
                else if (propType == typeof(Guid))
                {
                    convertedValue = Guid.Parse(value);
                }
                else
                {
                    convertedValue = Convert.ChangeType(value, propType);
                }

                property.SetValue(item, convertedValue);
            }
            catch
            {
                if (settings.ValidateImportData)
                {
                    throw;
                }
            }
        }
    }
}