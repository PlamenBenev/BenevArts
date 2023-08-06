using BenevArts.Common;
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
			if (!Validations.IsValidQuery(sortOrder))
			{
				return View();
			}

			IEnumerable<AssetViewModel> assets = await categoryService.
                GetAssetsByCategoryIdAsync(categoryId);

			ViewData["CurrentSortOrder"] = sortOrder;

			return View("~/Views/Asset/All.cshtml", 
                Pagination.Paginator(assets,null,categoryId,page,currentPage, sortOrder));
        }
    }
}
