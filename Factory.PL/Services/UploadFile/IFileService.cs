using Microsoft.AspNetCore.Mvc;

namespace Factory.PL.Services.UploadFile
{
    public interface IFileService
    {
        Task<string> UploadFileAsync(IFormFile file, string folderName);
        Task<FileStreamResult> DownloadFileAsync(string fileName);
        Task<bool> DeleteFileAsync(string fileName);
    }

}
