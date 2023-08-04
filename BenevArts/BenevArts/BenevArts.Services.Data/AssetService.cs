using AutoMapper;
using BenevArts.Common.Exeptions;
using BenevArts.Data;
using BenevArts.Data.Models;
using BenevArts.Services.Data.Interfaces;
using BenevArts.Web.ViewModels.Home;
using Microsoft.EntityFrameworkCore;

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
                    Seller = a.Seller.SellerName
                })
                .ToListAsync();
        }
        public async Task<IEnumerable<AssetViewModel>> GetSearchResultAsync(string query)
        {
            return await context.Assets
                .Where(a => a.Title.Contains(query) || a.Description.Contains(query))
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
        public async Task<AssetViewModel> GetAssetByIdAsync(Guid id, string userId)
        {
            Asset asset = await context.Assets
                .Include(a => a.Category)
                .Include(a => a.Seller)
                .Include(a => a.Images)
                .Include(a => a.Comments)
                 .ThenInclude(c => c.User)
                .Include(a => a.Likes)
                .Include(a => a.UserFavorites)
                .FirstOrDefaultAsync(a => a.Id == id)
                ?? throw new AssetNullException();

            // Mapping the model
            AssetViewModel viewModel = mapper.Map<AssetViewModel>(asset);

            // Mapping the images
            viewModel.Images = asset!.Images.Select(x => x.ImageName).ToList();

            if (userId != null)
            {
                // Check if the user liked the model
                List<Like> userLikes = await context.Likes
                    .Where(l => l.AssetId == id && l.UserId == Guid.Parse(userId))
                    .ToListAsync();

                viewModel.IsLikedByCurrentUser = userLikes.Any();

                // Check if the user added the model to Favorites
                List<UserFavorites> userFavorites = await context.UserFavorites
                    .Where(l => l.AssetId == id && l.UserId == Guid.Parse(userId))
                    .ToListAsync();

                viewModel.IsFavoritedByCurrentUser = userFavorites.Any();
            }

            return viewModel;
        }

        // Post

	}
}
