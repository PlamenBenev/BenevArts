using BenevArts.Web.ViewModels.Home;

namespace BenevArts.Services.Data.Interfaces
{
    public interface IAssetService
    {
        // Get
        Task<IEnumerable<AssetViewModel>> GetAllAssetsAsync();
        Task<IEnumerable<AssetViewModel>> GetSearchResultAsync(string query);
        Task<AssetViewModel> GetAssetByIdAsync(Guid id,string userId);

        // Post

	}
}
