using BenevArts.Data.Models;
using BenevArts.Services.Data.Interfaces;
using BenevArts.Web.ViewModels.Home;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NuGet.ContentModel;

namespace BenevArts.Web.Controllers
{
	public class AssetController : BaseController
	{
		private readonly IAssetService assetService;
		private readonly ILikeService likeService;
		private readonly ICategoryService categoryService;

		public AssetController(IAssetService _assetService,
			ILikeService _likeService,
			ICategoryService _categoryService)
		{
			assetService = _assetService;
			likeService = _likeService;
			categoryService = _categoryService;
		}

		// Get
		[HttpGet]
		[Authorize(Roles = "Seller,Admin")]
		public async Task<IActionResult> Add()
		{
			AddAssetViewModel model = new AddAssetViewModel()
			{
                Categories = await categoryService.GetCategoriesAsync(),
			};

			return View(model);
		}

		[HttpGet]
		[Authorize(Roles = "Seller,Admin")]
		public async Task<IActionResult> Edit(Guid assetId)
		{
			EditAssetViewModel model = await assetService.GetEditByIdAsync(assetId);

            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> All()
		{
			IEnumerable<AssetViewModel> models = await assetService.GetAllAssetsAsync();

			return View(models);
		}

        [HttpGet]
		[Authorize(Roles = "User,Seller,Admin")]
		public async Task<IActionResult> Favorites()
        {
            IEnumerable<AssetViewModel> models = await assetService.GetFavoritesAsync(GetUserId());

            return View("~/Views/Asset/All.cshtml", models);
        }

        [HttpGet]
		[Authorize(Roles = "Seller,Admin")]
		public async Task<IActionResult> MyStore()
        {
            throw new NotImplementedException();
        }

        [HttpGet]
		[AllowAnonymous]
		public async Task<IActionResult> Search(string query)
		{
			IEnumerable<AssetViewModel> models = await assetService.GetSearchResultAsync(query);

            return View("~/Views/Asset/All.cshtml", models);
        }

        [HttpGet]
		[AllowAnonymous]
		public async Task<IActionResult> Details(Guid id)
		{
			AssetViewModel model = await assetService.GetAssetByIdAsync(id, GetUserId());

			return View(model);
		}

		[HttpGet]
		[Authorize(Roles = "User,Seller,Admin")]
		public async Task<IActionResult> Download(Guid assetId)
		{
			AssetViewModel asset = await assetService.GetAssetByIdAsync(assetId, GetUserId());
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
		[Authorize(Roles = "Seller,Admin")]
		public async Task<IActionResult> Add(AddAssetViewModel model)
		{
			if (ModelState.IsValid)
			{
				await assetService.AddAssetAsync(model, GetUserId());

				return RedirectToAction(nameof(MyStore));
			}

			return View(model);
		}

        [HttpPost]
		[Authorize(Roles = "Seller,Admin")]
		public async Task<IActionResult> Edit(EditAssetViewModel model,Guid assetId)
        {
			model.Id = assetId;
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await assetService.EditAssetAsync(model);

            return RedirectToAction(nameof(Details), new {id = assetId});
        }


        [HttpPost]
		[Authorize(Roles = "Seller,Admin")]
		public async Task<IActionResult> Remove(Guid id)
		{
			await assetService.RemoveAssetAsync(id, GetUserId());

			return RedirectToAction(nameof(All));
		}

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
