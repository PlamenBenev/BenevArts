using BenevArts.Services.Data;
using BenevArts.Services.Data.Interfaces;
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

        [HttpPost]
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
