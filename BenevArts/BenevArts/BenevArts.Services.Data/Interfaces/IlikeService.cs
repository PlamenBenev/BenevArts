using BenevArts.Data.Models;

namespace BenevArts.Services.Data.Interfaces
{
	public interface ILikeService
	{
		// Get
		Task<int> GetLikeCountAsync(Guid assetId);
		Task<bool> IsLikedByUserAsync(Guid assetId, string userId);
		Task<Like> GetLikeByUserAsync(Guid assetId, string userId);

		// Post
		Task AddLikeAsync(Guid assetId, string userId);
		Task RemoveLikeAsync(Guid assetId, string userId);
	}

}
