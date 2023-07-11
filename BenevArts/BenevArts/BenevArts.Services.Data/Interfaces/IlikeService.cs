namespace BenevArts.Services.Data.Interfaces
{
	public interface ILikeService
	{
		Task AddLikeAsync(Guid assetId, string userId);
		Task RemoveLikeAsync(Guid assetId, string userId);
		Task<int> GetLikeCountAsync(Guid assetId);
	}

}
