using BenevArts.Web.ViewModels.Home;

namespace BenevArts.Services.Data.Interfaces
{
	public interface ICommentService
	{
		Task<IEnumerable<AssetViewModel>> GetCommentsAsync();
	}
}
