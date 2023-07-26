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
                Categories = await categoryService.GetCategoriesViewAsync(),
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
        public async Task<IActionResult> All(int page = 1, int itemsPerPage = 1)
        {
            IEnumerable<AssetViewModel> models = await assetService.GetAllAssetsAsync();

            return View(Pagination.Paginator(models, null, -1, page, itemsPerPage));
        }

        [HttpGet]
        [Authorize(Roles = "User,Seller,Admin")]
        public async Task<IActionResult> Favorites(int page = 1, int itemsPerPage = 1)
        {
            IEnumerable<AssetViewModel> models = await assetService.GetFavoritesAsync(GetUserId());

            return View("~/Views/Asset/All.cshtml", Pagination.Paginator(models, null, -1, page, itemsPerPage));

        }

        [HttpGet]
        [Authorize(Roles = "Seller,Admin")]
        public async Task<IActionResult> MyStore(int page = 1, int itemsPerPage = 1)
        {
            IEnumerable<AssetViewModel> models = await assetService.GetMyStoreAsync(GetUserId());

            return View("~/Views/Asset/All.cshtml", Pagination.Paginator(models,null, -1, page, itemsPerPage));
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Search(string query, int page = 1, int itemsPerPage = 1)
        {
            IEnumerable<AssetViewModel> models = await assetService.GetSearchResultAsync(query);

            return View(Pagination.Paginator(models, query,-1, page, itemsPerPage));
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
        [Authorize(Roles = "Seller,Admin")]
        public async Task<IActionResult> Add(AddAssetViewModel model)
        {
            CheckFormats(model);

            if (ModelState.IsValid)
            {
                await assetService.AddAssetAsync(model, GetUserId());

                return RedirectToAction(nameof(MyStore));
            }

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Seller,Admin")]
        public async Task<IActionResult> Edit(EditAssetViewModel model, Guid assetId)
        {
            model.Id = assetId;
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await assetService.EditAssetAsync(model);

            return RedirectToAction(nameof(Details), new { id = assetId });
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

        //public PaginatedAssetViewModel Paginator(IEnumerable<AssetViewModel> models, int page, int itemsPerPage)
        //{
        //    int totalItems = models.Count();

        //    // Calculate the number of assets to skip based on the current page and items per page
        //    int skip = (page - 1) * itemsPerPage;

        //    // Get the assets for the current page using Skip and Take LINQ methods
        //    var assetsForPage = models.Skip(skip).Take(itemsPerPage).ToList();

        //    // Create the view model for the current page of assets
        //    var paginatedViewModel = new PaginatedAssetViewModel
        //    {
        //        Assets = assetsForPage,
        //        CurrentPage = page,
        //        ItemsPerPage = itemsPerPage,
        //        TotalItems = totalItems
        //    };

        //    return paginatedViewModel;
        //}

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
