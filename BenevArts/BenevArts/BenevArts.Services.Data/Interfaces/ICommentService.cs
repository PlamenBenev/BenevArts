using BenevArts.Web.ViewModels.Home;

namespace BenevArts.Services.Data.Interfaces
{
	public interface ICommentService
	{
		// Post
		Task<CommentViewModel> AddCommentAsync(Guid assetId, string userId, string content);
		Task RemoveCommentAsync(int commentId, string userId);
	}
}
