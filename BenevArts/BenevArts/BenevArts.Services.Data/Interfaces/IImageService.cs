using Microsoft.AspNetCore.Http;
using System.Drawing;

namespace BenevArts.Services.Data.Interfaces
{
    public interface IImageService
    {
		Task<string> SaveImageAsync(IFormFile imageFile);
		Task<string> SaveThumbnailAsync(IFormFile imageFile);
	}
}
