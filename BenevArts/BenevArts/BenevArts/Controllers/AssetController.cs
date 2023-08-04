using BenevArts.Common;
using BenevArts.Services.Data.Interfaces;
using BenevArts.Web.Infrastructure;
using BenevArts.Web.ViewModels.Home;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace BenevArts.Web.Controllers
{
	public class AssetController : BaseController
	{
		private readonly IAssetService assetService;
		private readonly ILikeService likeService;

		public AssetController(IAssetService _assetService,
			ILikeService _likeService)
		{
			assetService = _assetService;
			likeService = _likeService;
		}

		// Get

		[HttpGet]
		[AllowAnonymous]
		public async Task<IActionResult> All(string sortOrder, int page = 1, int itemsPerPage = 1)
		{
			IEnumerable<AssetViewModel> models = await assetService.GetAllAssetsAsync();

			ViewData["CurrentSortOrder"] = sortOrder;

			return View(Pagination.Paginator(models, null, -1, page, itemsPerPage,sortOrder));
		}

		[HttpGet]
		[AllowAnonymous]
		public async Task<IActionResult> Search(string sortOrder,string query, int page = 1, int itemsPerPage = 1)
		{
			if (string.IsNullOrWhiteSpace(query))
			{
				return RedirectToAction(nameof(All));
			}
			if (!Validations.IsValidQuery(query))
			{
				ViewData["InvalidInput"] = "Invalid input. Only letters and numbers are allowed.";

				return View();
			}

			IEnumerable<AssetViewModel> models = await assetService.GetSearchResultAsync(query);

			if (models == null)
			{
				return View();
			}

			return View(Pagination.Paginator(models, query, -1, page, itemsPerPage, sortOrder));
		}

		[HttpGet]
		[AllowAnonymous]
		public async Task<IActionResult> Details(Guid id)
		{
			AssetViewModel model = await assetService.GetAssetByIdAsync(id, GetUserId());

			return View(model);
		}

		[HttpGet]
		[AllowAnonymous]
		public async Task<IActionResult> Download(Guid assetId)
		{
			if (!User.Identity?.IsAuthenticated ?? true)
			{
				return View("~/Areas/Identity/Pages/Account/Login.cshtml");
			}
			AssetViewModel asset = await assetService.GetAssetByIdAsync(assetId, GetUserId());

			string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "ZipFiles", asset.ZipFileName)
				?? throw new InvalidOperationException("The file was not found!");

			var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
			return File(fileStream, "application/zip", asset.ZipFileName);
		}

		// Post

		[HttpPost]
		[Authorize(Roles = "User,Seller,Admin")]
		public async Task<IActionResult> ToggleLike(Guid assetId, bool isLiked)
		{
			// Toggle the like status for the asset
			if (isLiked)
			{
				await likeService.RemoveLikeAsync(assetId, GetUserId());
			}
			else
			{
				await likeService.AddLikeAsync(assetId, GetUserId());
			}

			bool updatedIsLiked = await likeService.IsLikedByUserAsync(assetId, GetUserId());
			int updatedLikeCount = await likeService.GetLikeCountAsync(assetId);

			return Json(new { success = true, isLiked = updatedIsLiked, likeCount = updatedLikeCount });
		}

	}
}
