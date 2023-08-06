using BenevArts.Data.Models;
using BenevArts.Web.ViewModels.Home;

namespace BenevArts.Services.Data.Interfaces
{
	public interface ICategoryService
	{
        // Get
        Task<IEnumerable<AssetViewModel>> GetAssetsByCategoryIdAsync(int categoryId);
        Task<IEnumerable<CategoryViewModel>> GetCategoriesViewAsync();
        Task<IEnumerable<Category>> GetCategoriesAsync();
    }
}
