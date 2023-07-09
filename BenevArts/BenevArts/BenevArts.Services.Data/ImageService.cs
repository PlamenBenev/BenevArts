using BenevArts.Services.Data.Interfaces;
using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace BenevArts.Services.Data
{
	public class ImageService : IImageService
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

		public async Task<string> SaveThumbnailAsync(IFormFile imageFile)
		{
			var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
			var filePath = Path.Combine(uploadFolderPath, fileName);

			using (var stream = new FileStream(filePath, FileMode.Create))
			{
				await imageFile.CopyToAsync(stream);
			}

			using (var thumbnailImage = Image.Load(filePath))
			{
				thumbnailImage.Mutate(x => x.Resize(new ResizeOptions
				{
					Size = new Size(1500, 1500),
					Mode = ResizeMode.Crop
				}));

				thumbnailImage.Save(filePath);
			}



			return fileName;
		}
	}
}