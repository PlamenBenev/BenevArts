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
		public async Task<EditAssetViewModel> GetEditByIdAsync(Guid id)
		{

			Asset asset = await context.Assets.Where(a => a.Id == id).FirstOrDefaultAsync()
				?? throw new AssetNullException();

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
		public async Task AddAssetAsync(AddAssetViewModel model, string userId)
		{
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
			ImageService imageService = new ImageService(uploadFolderPath);

			// Add the Thumbnail
			string thumbName = await imageService.SaveThumbnailAsync(model.Thumbnail);
			asset.Thumbnail = thumbName;

			// Add all preview images
			foreach (var imageFile in model.Images)
			{
				string imageName = await imageService.SaveImageAsync(imageFile);

				AssetImage image = new AssetImage
				{
					ImageName = imageName,
					AssetId = asset.Id,
				};

				asset.Images.Add(image);
			}

			await context.Assets.AddAsync(asset);
			await context.SaveChangesAsync();
		}
		public async Task<bool> EditAssetAsync(EditAssetViewModel modelInputs)
		{
			Asset model = await context.Assets.FindAsync(modelInputs.Id)
				?? throw new AssetNullException();

			mapper.Map(modelInputs, model);

			await context.SaveChangesAsync();

			return true;
		}
		public async Task RemoveAssetAsync(Guid assetId, string userId)
		{
			ApplicationUser user = await context.Users
				.Where(u => u.Id.ToString() == userId)
				.FirstOrDefaultAsync()
				?? throw new UserNullException();

			Asset asset = await context.Assets.FirstOrDefaultAsync(a => a.Id == assetId)
				?? throw new AssetNullException();

			context.Assets.Remove(asset!);
			await context.SaveChangesAsync();

		}
	}
}
