using Factory.DAL.Models.Settings;

namespace Factory.PL.Services.DataExportImport
{
    public interface IImportService
    {
        Task<IEnumerable<T>> ImportDataAsync<T>(Stream fileStream, string format, ExportImportSettings settings = null) where T : new();
        bool IsFormatSupported(string format, ExportImportSettings settings = null);
        bool ValidateFileSize(long fileSize, ExportImportSettings settings = null);
    }
}
