using BenevArts.Common;
using BenevArts.Services.Data.Interfaces;
using BenevArts.Web.Infrastructure;
using BenevArts.Web.ViewModels.Home;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BenevArts.Web.Controllers
{
	public class FavoriteController : BaseController
	{
		private readonly IFavoriteService favoriteService;

        public FavoriteController(IFavoriteService _favoriteService)
        {
            favoriteService = _favoriteService;
        }

		[HttpGet]
		[Authorize(Roles = "User,Seller,Admin")]
		public async Task<IActionResult> Favorites(string sortOrder, int page = 1, int itemsPerPage = 1)
		{
			if (!Validations.IsValidQuery(sortOrder))
			{
				return View();
			}

			IEnumerable<AssetViewModel> models = await favoriteService.GetFavoritesAsync(GetUserId());

			ViewData["CurrentSortOrder"] = sortOrder;

			return View("~/Views/Asset/All.cshtml", Pagination.Paginator(models, null, -1, page, itemsPerPage, sortOrder));

		}

		[HttpPost]
		[Authorize(Roles = "User,Seller,Admin")]
		public async Task<IActionResult> ToggleFavorite(Guid assetId, bool isFavorited)
		{
			if (isFavorited)
			{
				await favoriteService.RemoveFromFavoriteAsync(assetId, GetUserId());
			}
			else
			{
				await favoriteService.AddToFavoriteAsync(assetId, GetUserId());
			}

			bool updatedIsFavorited = await favoriteService.IsFavoritedByUserAsync(assetId, GetUserId());

			return Json(new { success = true, isFavorited = updatedIsFavorited });
		}
	}
}
