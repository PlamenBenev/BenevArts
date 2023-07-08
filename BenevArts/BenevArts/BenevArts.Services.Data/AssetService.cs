using AutoMapper;
using BenevArts.Data;
using BenevArts.Data.Models;
using BenevArts.Services.Data.Interfaces;
using BenevArts.Web.ViewModels.Home;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Net.Mime.MediaTypeNames;

namespace BenevArts.Services.Data
{
	public class AssetService : IAssetService
	{
		private readonly BenevArtsDbContext context;
		private readonly IMapper mapper;
		public AssetService(BenevArtsDbContext _context, IMapper _mapper)
		{
			context = _context;
			mapper = _mapper;
		}

		// Get
		public async Task<IEnumerable<AssetViewModel>> GetAllAssetsAsync()
		{
			return await context.Assets
				.Select(a => new AssetViewModel
				{
					Id = a.Id,
					Title = a.Title,
					Thumbnail = a.Thumbnail,
					Price = a.Price,
					UploadDate = a.UploadDate,
					Seller = a.Seller.Name
				})
				.ToListAsync();
		}
		public async Task<IEnumerable<AssetViewModel>> GetSearchResultAsync(string query)
		{
			return await context.Assets
				.Where(a => a.Title.Contains(query) || a.Description.Contains(query))
				.Select(a => new AssetViewModel
				{
					Title = a.Title,
					Thumbnail = a.Thumbnail,
					Price = a.Price,
					UploadDate = a.UploadDate,
					Seller = a.Seller.Name
				})
				.ToListAsync();
		}
		public async Task<IEnumerable<Category>> GetCategoriesAsync()
		{
			return await context.Categories.ToListAsync();
		}
		public async Task<AssetViewModel> GetAssetByIdAsync(Guid id)
		{
			Asset? asset = await context.Assets
				.Include(a => a.Category)
				.Include(a => a.Seller)
				.Include(a => a.Images)
				.FirstOrDefaultAsync(a => a.Id == id);

			if (asset == null)
			{

			}

			AssetViewModel viewModel = mapper.Map<AssetViewModel>(asset);

			viewModel.Images = asset!.Images.Select(x => x.ImageName).ToList();

			return viewModel;
		}

		// Post
		public async Task AddAssetAsync(AddAssetViewModel model, string userId, string username, string email)
		{
			Seller? seller = await context.Sellers.FindAsync(Guid.Parse(userId));

			// Add the Seller in the database if it's not already added
			if (seller == null)
			{
				seller = new Seller
				{
					Id = Guid.Parse(userId),
					Name = username,
					Email = email,
					UserId = Guid.Parse(userId),
				};
				await context.Sellers.AddAsync(seller);
				await context.SaveChangesAsync();
			}

			// Uploading the Zip file
			var fileName = Path.GetFileName(model.ZipFileName.FileName);
			var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "ZipFiles", fileName);

			using (var fileStream = new FileStream(filePath, FileMode.Create))
			{
				await model.ZipFileName.CopyToAsync(fileStream);
			}

			// Map the Asset
			Asset asset = mapper.Map<Asset>(model);
			asset.SellerId = Guid.Parse(userId);
			asset.UploadDate = DateTime.UtcNow;

			// Set the path for the images
			var uploadFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images");
			var imageService = new ImageService(uploadFolderPath);

			// Add the Thumbnail
			var thumbName = await imageService.SaveImageAsync(model.Thumbnail);
			asset.Thumbnail = thumbName;

			// Add all preview images
			foreach (var imageFile in model.Images)
			{
				var imageName = await imageService.SaveImageAsync(imageFile);

				var image = new AssetImage
				{
					ImageName = imageName,
					AssetId = asset.Id,
				};

				asset.Images.Add(image);
			}

			await context.Assets.AddAsync(asset);
			await context.SaveChangesAsync();
		}
		public async Task RemoveAssetAsync(Guid assetId, string userId)
		{
			var user = await context.Users
				.Where(u => u.Id.ToString() == userId)
				.FirstOrDefaultAsync();

			var asset = await context.Assets.FirstOrDefaultAsync(a => a.Id == assetId);

			context.Assets.Remove(asset);
			await context.SaveChangesAsync();

		}

	}
}
