using BenevArts.Data.Models;
using BenevArts.Web.ViewModels.Home;

namespace BenevArts.Services.Data.Interfaces
{
    public interface IAssetService
    {
        // Get
        Task<IEnumerable<Category>> GetCategoriesAsync();
        Task<IEnumerable<AssetViewModel>> GetAllAssetsAsync();
        Task<IEnumerable<AssetViewModel>> GetSearchResultAsync(string query);
        Task<AssetViewModel> GetAssetByIdAsync(Guid id);

        // Post
        Task AddAssetAsync(AddAssetViewModel model, string userId, string username, string email);
        Task RemoveAssetAsync(Guid assetId, string userId);

	}
}
