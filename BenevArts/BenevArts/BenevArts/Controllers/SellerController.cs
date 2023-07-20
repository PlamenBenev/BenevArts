using BenevArts.Data.Models;
using BenevArts.Services.Data;
using BenevArts.Services.Data.Interfaces;
using BenevArts.Web.ViewModels.Home;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace BenevArts.Web.Controllers
{
	public class SellerController : BaseController
	{
		private readonly ISellerService sellerService;

        public SellerController(ISellerService _sellerService)
        {
            sellerService = _sellerService;
        }

        // GET
        [HttpGet]
		[Authorize(Roles = "User,Admin")]
		public IActionResult Apply()
		{
			return View();
		}

		[HttpGet]
		[Authorize(Roles = "User,Admin")]
		public IActionResult ThankYou()
		{
			return View();
		}

		[HttpGet]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> AllApplications()
		{
			IEnumerable<SellerApplicationViewModel> models = await sellerService.GetAllApplicationsAsync();

			return View("~/Views/Seller/AllApplications.cshtml",models);
		}

		[HttpGet]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> SingleApplication(int id)
		{
			SellerApplicationViewModel model = await sellerService.GetSingleApplicationAsync(id);

			return View("~/Views/Seller/SingleApplication.cshtml",model);
		}

		// POST
		[HttpPost]
		[Authorize(Roles = "User,Admin")]
		public async Task<IActionResult> Apply(SellerApplicationViewModel application)
		{
			if (ModelState.IsValid)
			{
				await sellerService.ApplyAsync(application,GetUserId());

				return RedirectToAction(nameof(ThankYou));
			}

			return View(application);
		}


	}
}
