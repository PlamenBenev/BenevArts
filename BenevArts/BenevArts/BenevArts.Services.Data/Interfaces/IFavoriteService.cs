using BenevArts.Data.Models;
using BenevArts.Web.ViewModels.Home;

namespace BenevArts.Services.Data.Interfaces
{
	public interface IFavoriteService
	{
		Task<IEnumerable<AssetViewModel>> GetFavoritesAsync(string userId);
		Task AddToFavoriteAsync(Guid assetId, string userId);
		Task<UserFavorites> GetFavoriteByUserAsync(Guid assetId, string userId);
		Task<bool> IsFavoritedByUserAsync(Guid assetId, string userId);
		Task RemoveFromFavoriteAsync(Guid assetId, string userId);
	}
}
