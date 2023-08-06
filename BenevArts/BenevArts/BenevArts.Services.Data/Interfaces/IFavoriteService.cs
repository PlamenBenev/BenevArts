using BenevArts.Data.Models;
using BenevArts.Web.ViewModels.Home;

namespace BenevArts.Services.Data.Interfaces
{
	public interface IFavoriteService
	{
		// Get
		Task<IEnumerable<AssetViewModel>> GetFavoritesAsync(string userId);
		Task<UserFavorites> GetFavoriteByUserAsync(Guid assetId, string userId);
		Task<bool> IsFavoritedByUserAsync(Guid assetId, string userId);
		// Post
		Task AddToFavoriteAsync(Guid assetId, string userId);
		Task RemoveFromFavoriteAsync(Guid assetId, string userId);
	}
}
