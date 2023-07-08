﻿using AutoMapper;
using BenevArts.Services.Data.Interfaces;
using BenevArts.Web.ViewModels.Home;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BenevArts.Web.Controllers
{
    public class AssetController : BaseController
    {
        private readonly IAssetService assetService;
        private readonly IImageService imageService;

        public AssetController(IAssetService _assetService,
            IImageService _imageService)
        {
            assetService = _assetService;
            imageService = _imageService;
        }

        // Get
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var model = new AddAssetViewModel()
            {
                Categories = await assetService.GetCategoriesAsync(),
            };

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var models = await assetService.GetAllAssetsAsync();

            return View(models);
        }

        [HttpGet]
        public async Task<IActionResult> Search(string query)
        {
            var models = await assetService.GetSearchResultAsync(query);

            return View(models);
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var model = await assetService.GetAssetByIdAsync(id);

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Download(Guid id)
        {
            var asset = await assetService.GetAssetByIdAsync(id);
            if (asset == null)
            {
                return NotFound();
            }

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "ZipFiles", asset.ZipFileName);
            if (!System.IO.File.Exists(filePath))
            {
                return NotFound();
            }

            var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            return File(fileStream, "application/zip", asset.ZipFileName);
        }

        // Post
        [HttpPost]
        public async Task<IActionResult> Add( AddAssetViewModel model)
        {
            if (ModelState.IsValid)
            {
                await assetService.AddAssetAsync(model, GetUserId(),GetUsername(),GetEmail());

                return RedirectToAction(nameof(All));
            }

            return View(model);
        }     
    }
}
