using BenevArts.Data.Models;

namespace BenevArts.Services.Data.Interfaces
{
	public interface IFavoriteService
	{
		Task AddToFavoriteAsync(Guid assetId, string userId);
		Task<UserFavorites> GetFavoriteByUserAsync(Guid assetId, string userId);
		Task<bool> IsFavoritedByUserAsync(Guid assetId, string userId);
		Task RemoveFromFavoriteAsync(Guid assetId, string userId);
	}
}
