using BenevArts.Services.Data.Interfaces;
using BenevArts.Web.ViewModels.Home;
using Microsoft.AspNetCore.Mvc;

namespace BenevArts.Web.Controllers
{
    public class AssetController : Controller
    {
        private readonly IAssetService assetService;

        public AssetController(IAssetService _assetService)
        {
               assetService = _assetService;
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var model = new AddAssetViewModel()
            {
                Categories = await assetService.GetCategoriesAsync(),
            };

            return View(model);
        }

    }
}
