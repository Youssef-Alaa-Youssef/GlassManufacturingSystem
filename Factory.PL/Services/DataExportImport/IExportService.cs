using Factory.DAL.Models.Settings;

namespace Factory.PL.Services.DataExportImport
{
    public interface IExportService
    {
        Task<byte[]> ExportDataAsync<T>(IEnumerable<T> data, string format, ExportImportSettings settings = null);
        string GetContentType(string format);
        string GetFileExtension(string format);
        bool IsFormatSupported(string format, ExportImportSettings settings = null);
    }
}
