using BenevArts.Data.Models;

namespace BenevArts.Services.Data.Interfaces
{
	public interface ILikeService
	{
		Task AddLikeAsync(Guid assetId, string userId);
		Task RemoveLikeAsync(Guid assetId, string userId);
		Task<int> GetLikeCountAsync(Guid assetId);
		Task<bool> IsLikedByUserAsync(Guid assetId, string userId);
		Task<Like> GetLikeByUserAsync(Guid assetId, string userId);

	}

}
