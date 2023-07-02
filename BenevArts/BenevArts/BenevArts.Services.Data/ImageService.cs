using Microsoft.AspNetCore.Http;

namespace BenevArts.Services.Data
{
    public class ImageService
    {
        private readonly string uploadFolderPath;

        public ImageService(string _uploadFolderPath)
        {
            uploadFolderPath = _uploadFolderPath;
        }

        public async Task<string> SaveImageAsync(IFormFile imageFile)
        {
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
            var filePath = Path.Combine(uploadFolderPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }

            return fileName;
        }

        public string GetImagePath(string fileName)
        {
            return Path.Combine(uploadFolderPath, fileName);
        }

        // Other methods for image retrieval, deletion, and other file system operations
        // ...
    }
}