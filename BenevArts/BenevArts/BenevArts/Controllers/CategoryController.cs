using BenevArts.Services.Data.Interfaces;
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
        public async Task<IActionResult> Assets(int categoryId)
        {
            IEnumerable<AssetViewModel> assets = await categoryService.
                GetAssetsByCategoryIdAsync(categoryId);

            return View("~/Views/Asset/All.cshtml", assets);
        }
    }
}
