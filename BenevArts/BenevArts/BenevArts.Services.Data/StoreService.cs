using AutoMapper;
using BenevArts.Common.Exeptions;
using BenevArts.Data;
using BenevArts.Data.Models;
using BenevArts.Services.Data.Interfaces;
using BenevArts.Web.ViewModels.Home;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BenevArts.Services.Data
{
	public class StoreService : IStoreService
	{
		private readonly BenevArtsDbContext context;
		private readonly UserManager<ApplicationUser> userManager;
		private readonly IMapper mapper;

		public StoreService(BenevArtsDbContext _context,
			UserManager<ApplicationUser> _userManager,
			 IMapper _mapper)
		{
			context = _context;
			userManager = _userManager;
			mapper = _mapper;
		}
		// Get
		public async Task<IEnumerable<AssetViewModel>> GetMyStoreAsync(string userId)
		{
			return await context.Assets
				.Where(f => f.SellerId == Guid.Parse(userId))
				.Select(a => new AssetViewModel
				{
					Id = a.Id,
					Title = a.Title,
					Thumbnail = a.Thumbnail,
					Price = a.Price,
					UploadDate = a.UploadDate,
					Seller = a.Seller.SellerName
				})
				.ToListAsync();
		}
		public async Task<EditAssetViewModel> GetEditByIdAsync(Guid id, string userId)
		{
			Asset asset = await context.Assets.Where(a => a.Id == id).FirstOrDefaultAsync()
				?? throw new AssetNullException();

			if (asset.SellerId != Guid.Parse(userId))
			{
				throw new InvalidOperationException("The User does not own the Asset");
			}

			IEnumerable<CategoryViewModel> categories = await context.Categories
				.Select(ct => new CategoryViewModel
				{
					Id = ct.Id,
					Name = ct.Name,
				}).ToListAsync();

			EditAssetViewModel model = mapper.Map<EditAssetViewModel>(asset);
			model.Categories = categories;
			return model;
		}

		// Post
		public async Task AddAssetAsync(BaseAssetViewModel model, string userId)
		{
			// Map the Asset
			Asset asset = mapper.Map<Asset>(model);
			asset.SellerId = Guid.Parse(userId);
			asset.UploadDate = DateTime.UtcNow;

			await UploadFilesAsync(asset, model);

			await context.Assets.AddAsync(asset);
			await context.SaveChangesAsync();
		}
		public async Task EditAssetAsync(EditAssetViewModel modelInputs, string userId)
		{
			Asset asset = await context.Assets.FindAsync(modelInputs.Id)
				?? throw new AssetNullException();

			// Check if the user owns the Asset
			if (asset.SellerId != Guid.Parse(userId))
			{
				throw new InvalidOperationException("The User does not own the Asset");
			}

			// Delete existing images
			List<AssetImage> images = await context.AssetImages.Where(a => a.AssetId == modelInputs.Id)
				.ToListAsync();

			for (int i = images.Count - 1; i >= 0; i--)
			{
				AssetImage image = images[i];
				string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", image.ImageName);

				if (File.Exists(folderPath))
				{
					File.Delete(folderPath);
				}
				context.AssetImages.Remove(image);
			}

			asset.Title = modelInputs.Title;
			asset.Description = modelInputs.Description;
			asset.CGIModel = modelInputs.CGIModel;
			asset.Animated = modelInputs.Animated;
			asset.CategoryId = modelInputs.CategoryId;
			asset.LowPoly = modelInputs.LowPoly;
			asset.Materials = modelInputs.Materials;
			asset.PBR = modelInputs.PBR;
			asset.Price = modelInputs.Price;
			asset.Rigged = modelInputs.Rigged;
			asset.Textures = modelInputs.Textures;
			asset.UVUnwrapped = modelInputs.UVUnwrapped;
			asset.CategoryId = modelInputs.CategoryId;
			asset.SellerId = Guid.Parse(userId);


			await UploadFilesAsync(asset, modelInputs);
			await context.SaveChangesAsync();
		}
		public async Task RemoveAssetAsync(Guid assetId, string userId)
		{
			ApplicationUser user = await context.Users
				.Where(u => u.Id.ToString() == userId)
				.FirstOrDefaultAsync()
				?? throw new UserNullException();

			Asset asset = await context.Assets.FirstOrDefaultAsync(a => a.Id == assetId)
				?? throw new AssetNullException();

			if (asset.SellerId == user.Id)
			{
				context.Assets.Remove(asset!);
				await context.SaveChangesAsync();
			}
		}

		private async Task UploadFilesAsync(Asset asset, BaseAssetViewModel modelInputs)
		{
			// Uploading the Zip file
			var fileName = Path.GetFileName(modelInputs.ZipFileName.FileName);
			var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "ZipFiles", fileName);

			using (var fileStream = new FileStream(filePath, FileMode.Create))
			{
				await modelInputs.ZipFileName.CopyToAsync(fileStream);
			}

			// Set the path for the images
			string uploadFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images");
			ImageService imageService = new ImageService(uploadFolderPath);

			// Add the Thumbnail
			string thumbName = await imageService.SaveThumbnailAsync(modelInputs.Thumbnail);
			asset.Thumbnail = thumbName;

			await context.SaveChangesAsync();

			// Add all preview images
			foreach (var imageFile in modelInputs.Images)
			{
				string imageName = await imageService.SaveImageAsync(imageFile);

				AssetImage image = new AssetImage
				{
					ImageName = imageName,
					AssetId = asset.Id,
				};

				asset.Images.Add(image);
			}
			await context.SaveChangesAsync();
		}
	}
}
