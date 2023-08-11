using BenevArts.Common;
using BenevArts.Services.Data.Interfaces;
using BenevArts.Web.Infrastructure;
using BenevArts.Web.ViewModels.Home;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BenevArts.Web.Controllers
{
	public class StoreController : BaseController
	{
		private readonly IStoreService storeService;
		private readonly ICategoryService categoryService;
		private readonly ILogger<StoreController> logger;

		public StoreController(IStoreService _storeService,
			ICategoryService _categoryService,
			ILogger<StoreController> _logger)
		{
			storeService = _storeService;
			categoryService = _categoryService;
			logger = _logger;
		}

		// Get

		[HttpGet]
		[Authorize(Roles = "Seller,Admin")]
		public async Task<IActionResult> Add()
		{
			try
			{
				AddAssetViewModel model = new AddAssetViewModel()
				{
					Categories = await categoryService.GetCategoriesViewAsync(),
				};

				return View(model);
			}
			catch (Exception ex)
			{
				logger.LogError(ex, "An exception occurred in the Add (Get) action.");
				return View("Error");
			}
		}

		[HttpGet]
		[Authorize(Roles = "Seller,Admin")]
		public async Task<IActionResult> Edit(Guid assetId)
		{
			try
			{
				EditAssetViewModel model = await storeService.GetEditByIdAsync(assetId,GetUserId());

				return View(model);
			}
			catch (Exception ex)
			{
				logger.LogError(ex, "An exception occurred in the Edit (Get) action.");
				return View("Error");
			}
		}

		[HttpGet]
		[Authorize(Roles = "Seller,Admin")]
		public async Task<IActionResult> MyStore(string sortOrder, int page = 1, int itemsPerPage = 1)
		{
			try
			{
				if (!Validations.IsValidQuery(sortOrder))
				{
					return View();
				}

				IEnumerable<AssetViewModel> models = await storeService.GetMyStoreAsync(GetUserId());

				ViewData["CurrentSortOrder"] = sortOrder;

				return View("~/Views/Asset/All.cshtml", Pagination.Paginator(models, null, -1, page, itemsPerPage, sortOrder));
			}
			catch (Exception ex)
			{
				logger.LogError(ex, "An exception occurred in the MyStore action.");
				return View("Error");
			}
		}

		// Post

		[HttpPost]
		[Authorize(Roles = "Seller,Admin")]
		public async Task<IActionResult> Add(AddAssetViewModel model)
		{
			try
			{
				CheckFormats(model);

				if (ModelState.IsValid)
				{
					await storeService.AddAssetAsync(model, GetUserId());

					return RedirectToAction(nameof(MyStore));
				}

				return View(model);
			}
			catch (Exception ex)
			{
				logger.LogError(ex, "An exception occurred in the Add (post) action.");
				return View("Error");
			}
		}

		[HttpPost]
		[Authorize(Roles = "Seller,Admin")]
		public async Task<IActionResult> Edit(EditAssetViewModel model, Guid assetId)
		{
			try
			{
				model.Id = assetId;

				if (!ModelState.IsValid)
				{
					return View(model);
				}

				await storeService.EditAssetAsync(model,GetUserId());

				return RedirectToAction("Details", "Asset", new { id = assetId });
			}
			catch (Exception ex)
			{
				logger.LogError(ex, "An exception occurred in the Edit (post) action.");
				return View("Error");
			}
		}

		[HttpPost]
		[Authorize(Roles = "Seller,Admin")]
		public async Task<IActionResult> Remove(Guid assetId)
		{
			try
			{
				await storeService.RemoveAssetAsync(assetId, GetUserId());
				
				return RedirectToAction(nameof(MyStore));
			}
			catch (Exception ex)
			{
				logger.LogError(ex, "An exception occurred in the Remove action.");
				return View("Error");
			}
		}

		private IActionResult CheckFormats(AddAssetViewModel model)
		{
			// Check if the thumbnail is in valid format
			if (model.Thumbnail == null || model.Thumbnail.Length == 0)
			{
				ModelState.AddModelError("Thumbnail", "Please select an image file to upload.");
				return View();
			}

			string[] allowedExtensions = { ".png", ".jpeg", ".jpg" };
			string fileExtension = Path.GetExtension(model.Thumbnail.FileName);

			if (!allowedExtensions.Contains(fileExtension.ToLowerInvariant()))
			{
				ModelState.AddModelError("Thumbnail", "Invalid file format! Please choose a PNG or JPEG image.");
				return View();
			}

			// Check if the Zip file is in valid format
			if (model.ZipFileName == null || model.ZipFileName.Length == 0)
			{
				ModelState.AddModelError("ZipFile", "Please select zip file to upload.");
				return View();
			}

			allowedExtensions = new string[] { ".zip", ".rar" };
			fileExtension = Path.GetExtension(model.ZipFileName.FileName);

			if (!allowedExtensions.Contains(fileExtension.ToLowerInvariant()))
			{
				ModelState.AddModelError("ZipFile", "Invalid file format! Please choose a zip or rar image.");
				return View();
			}

			// Check if all images are in valid format
			if (model.Images == null || model.Images.Count() == 0)
			{
				ModelState.AddModelError("ImageFiles", "Please select at least one image file to upload.");
				return View();
			}

			allowedExtensions = new string[] { ".png", ".jpeg", ".jpg" };

			foreach (var imageFile in model.Images)
			{
				if (imageFile.Length == 0)
				{
					ModelState.AddModelError("ImageFiles", "Please select a valid image file to upload.");
					return View();
				}

				fileExtension = Path.GetExtension(imageFile.FileName);

				if (!allowedExtensions.Contains(fileExtension.ToLowerInvariant()))
				{
					ModelState.AddModelError("ImageFiles", "Invalid file format! Please choose PNG or JPEG images only.");
					return View();
				}

			}
			return View(model);
		}
	}
}
