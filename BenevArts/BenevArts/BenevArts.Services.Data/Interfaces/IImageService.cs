using Microsoft.AspNetCore.Http;

namespace BenevArts.Services.Data.Interfaces
{
    public interface IImageService
    {
        Task<string> SaveImageAsync(IFormFile imageFile);
    }
}
