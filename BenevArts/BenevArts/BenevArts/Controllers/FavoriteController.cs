using BenevArts.Services.Data;
using BenevArts.Services.Data.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

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
