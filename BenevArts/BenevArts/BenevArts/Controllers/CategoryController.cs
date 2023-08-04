using BenevArts.Services.Data.Interfaces;
using BenevArts.Web.Infrastructure;
using BenevArts.Web.ViewModels.Home;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BenevArts.Web.Controllers
{
	public class CategoryController : BaseController
	{
		private readonly ICategoryService categoryService;

        public CategoryController(ICategoryService _categoryService)
        {
            categoryService = _categoryService;
		}

		[HttpGet]
		[AllowAnonymous]
		public async Task<IActionResult> Index()
		{
			IEnumerable<CategoryViewModel> categories = await categoryService.GetCategoriesViewAsync();

            return View(categories);
		}


        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Assets(string sortOrder, int categoryId,int page = 1, int currentPage = 1)
        {
            IEnumerable<AssetViewModel> assets = await categoryService.
                GetAssetsByCategoryIdAsync(categoryId);

            return View("~/Views/Category/AssetsInCategory.cshtml", 
                Pagination.Paginator(assets,null,categoryId,page,currentPage, sortOrder));
        }
    }
}
