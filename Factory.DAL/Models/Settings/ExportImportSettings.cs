
namespace Factory.DAL.Models.Settings
{
    public class ExportImportSettings
    {
        public int Id { get; set; }
        public bool EnableExport { get; set; } = true;
        public bool EnableImport { get; set; } = true;
        public List<string> SupportedExportFormats { get; set; } = new List<string> { "XLSX", "JSON", "XML", "CSV" };
        public List<string> SupportedImportFormats { get; set; } = new List<string> { "XLSX", "JSON", "XML", "CSV" };
        public int MaxExportRows { get; set; } = 10000;
        public int MaxImportFileSize { get; set; } = 10 * 1024 * 1024; // 10MB
        public bool IncludeHeaders { get; set; } = true;
        public string DateFormat { get; set; } = "yyyy-MM-dd";
        public string DefaultExportFormat { get; set; } = "XLSX";
        public char CsvDelimiter { get; set; } = ',';
        public bool AllowNullValues { get; set; } = true;
        public bool ValidateImportData { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
    }
}
