using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;


namespace Factory.PL.Services.UploadFile
{
    public class FileService : IFileService
    {
        private readonly string _fileStoragePath;
        private const long MaxFileSize = 5 * 1024 * 1024;

        public FileService(IWebHostEnvironment webHostEnvironment)
        {
            _fileStoragePath = Path.Combine(webHostEnvironment.WebRootPath, "images");

            if (!Directory.Exists(_fileStoragePath))
            {
                Directory.CreateDirectory(_fileStoragePath);
            }
        }

        public async Task<string> UploadFileAsync(IFormFile file, string folderName)
        {
            if (file == null || file.Length == 0)
                return "No file uploaded. Please select a video file.";

            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".bmp" };
            var fileExtension = Path.GetExtension(file.FileName).ToLower();

            if (!Array.Exists(allowedExtensions, ext => ext == fileExtension))
            {
                throw new InvalidOperationException("Invalid file type. Only image files are allowed.");
            }

            if (file.Length > MaxFileSize)
            {
                throw new InvalidOperationException("File size exceeds 5MB.");
            }

            var folderPath = Path.Combine(_fileStoragePath, folderName);

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            var fileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(file.FileName);

            var filePath = Path.Combine(folderPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var relativeFilePath = Path.Combine("images", folderName, fileName);
            return $"/{relativeFilePath}";
        }

        public async Task<FileStreamResult> DownloadFileAsync(string fileName)
        {
            var filePath = Path.Combine(_fileStoragePath, fileName);

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("File not found.");
            }

            var memory = new MemoryStream();
            using (var stream = new FileStream(filePath, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;

            var contentType = GetContentType(fileName);

            return new FileStreamResult(memory, contentType)
            {
                FileDownloadName = fileName
            };
        }

        public Task<bool> DeleteFileAsync(string fileName)
        {
            var filePath = Path.Combine(_fileStoragePath, fileName);

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                return Task.FromResult(true);
            }

            return Task.FromResult(false);
        }


        private string GetContentType(string fileName)
        {
            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(fileName, out var contentType))
            {
                contentType = "application/octet-stream";
            }
            return contentType;
        }
    }
}
