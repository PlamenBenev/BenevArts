using BenevArts.Web.ViewModels.Home;

namespace BenevArts.Services.Data.Interfaces
{
	public interface ICategoryService
	{
        Task<IEnumerable<AssetViewModel>> GetAssetsByCategoryIdAsync(int categoryId);
        Task<IEnumerable<CategoryViewModel>> GetCategoriesAsync();

	}
}
