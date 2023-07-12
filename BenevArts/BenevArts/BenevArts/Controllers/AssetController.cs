using BenevArts.Data.Models;
using BenevArts.Services.Data.Interfaces;
using BenevArts.Web.ViewModels.Home;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BenevArts.Web.Controllers
{
	public class AssetController : BaseController
	{
		private readonly IAssetService assetService;
		private readonly ILikeService likeService;
		public AssetController(IAssetService _assetService, ILikeService _likeService)
		{
			assetService = _assetService;
			likeService = _likeService;
		}

		// Get
		[HttpGet]
		public async Task<IActionResult> Add()
		{
			AddAssetViewModel model = new AddAssetViewModel()
			{
				Categories = await assetService.GetCategoriesAsync(),
			};

			return View(model);
		}

		[HttpGet]
		public async Task<IActionResult> Edit()
		{
			AddAssetViewModel model = new AddAssetViewModel()
			{
				Categories = await assetService.GetCategoriesAsync(),
			};

			return View(model);
		}

		[HttpGet]
		public async Task<IActionResult> All()
		{
			IEnumerable<AssetViewModel> models = await assetService.GetAllAssetsAsync();

			return View(models);
		}

		[HttpGet]
		public async Task<IActionResult> Search(string query)
		{
			IEnumerable<AssetViewModel> models = await assetService.GetSearchResultAsync(query);

			return View(models);
		}

		[HttpGet]
		public async Task<IActionResult> Details(Guid id)
		{
			AssetViewModel model = await assetService.GetAssetByIdAsync(id, GetUserId());

			return View(model);
		}

		[HttpGet]
		public async Task<IActionResult> Download(Guid id)
		{
			AssetViewModel asset = await assetService.GetAssetByIdAsync(id, GetUserId());
			if (asset == null)
			{
				return NotFound();
			}

			string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "ZipFiles", asset.ZipFileName);
			if (!System.IO.File.Exists(filePath))
			{
				return NotFound();
			}

			var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
			return File(fileStream, "application/zip", asset.ZipFileName);
		}

		// Post
		[HttpPost]
		public async Task<IActionResult> Add(AddAssetViewModel model)
		{
			if (ModelState.IsValid)
			{
				await assetService.AddAssetAsync(model, GetUserId(), GetUsername(), GetEmail());

				return RedirectToAction(nameof(All));
			}

			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Remove(Guid id)
		{
			await assetService.RemoveAssetAsync(id, GetUserId());

			return RedirectToAction(nameof(All));
		}

		[HttpPost]
		public async Task<IActionResult> ToggleLike(Guid assetId, bool isLiked)
		{
			// Toggle the like status for the asset
			if (isLiked)
			{
				// Unlike the asset
				await likeService.RemoveLikeAsync(assetId, GetUserId());
			}
			else
			{
				// Like the asset
				await likeService.AddLikeAsync(assetId, GetUserId());
			}

			bool updatedIsLiked = await likeService.IsLikedByUserAsync(assetId, GetUserId());

			// Return a JSON response indicating success and the updated like status
			return Json(new { success = true, isLiked = updatedIsLiked });
		}


		//[HttpPost]
		//public async Task<IActionResult> AddLike(Guid assetId)
		//{
		//	await likeService.AddLikeAsync(assetId, GetUserId());

		//	return RedirectToAction(nameof(Details), new { id = assetId });
		//}

		//[HttpPost]
		//public async Task<IActionResult> RemoveLike(Guid assetId)
		//{
		//	await likeService.RemoveLikeAsync(assetId, GetUserId());

		//	return RedirectToAction(nameof(Details), new { id = assetId });
		//}

	}
}
