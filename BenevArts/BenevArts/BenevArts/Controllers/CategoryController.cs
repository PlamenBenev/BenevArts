using BenevArts.Common;
using BenevArts.Services.Data.Interfaces;
using BenevArts.Web.Infrastructure;
using BenevArts.Web.ViewModels.Home;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BenevArts.Web.Controllers
{
	public class CategoryController : BaseController
	{
		private readonly ICategoryService categoryService;
		private readonly ILogger<CategoryController> logger;

		public CategoryController(ICategoryService _categoryService, ILogger<CategoryController> _logger)
		{
			categoryService = _categoryService;
			logger = _logger;
		}

		[HttpGet]
		[AllowAnonymous]
		public async Task<IActionResult> Index()
		{
			try
			{
				IEnumerable<CategoryViewModel> categories = await categoryService.GetCategoriesViewAsync();

				return View(categories);
			}
			catch (Exception ex)
			{
				logger.LogError(ex, "An exception occurred in the Index action.");
				return View("Error");
			}
		}


		[HttpGet]
		[AllowAnonymous]
		public async Task<IActionResult> Assets(string sortOrder, int categoryId, int page = 1, int currentPage = 1)
		{
			try
			{
				if (!Validations.IsValidQuery(sortOrder))
				{
					return View();
				}

				IEnumerable<AssetViewModel> assets = await categoryService.
					GetAssetsByCategoryIdAsync(categoryId);

				ViewData["CurrentSortOrder"] = sortOrder;

				return View("~/Views/Asset/All.cshtml",
					Pagination.Paginator(assets, null, categoryId, page, currentPage, sortOrder));
			}
			catch (Exception ex)
			{
				logger.LogError(ex, "An exception occurred in the Assets action.");
				return View("Error");
			}
		}
	}
}
