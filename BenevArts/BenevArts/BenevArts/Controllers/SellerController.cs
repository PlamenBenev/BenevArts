using BenevArts.Data.Models;
using BenevArts.Services.Data;
using BenevArts.Services.Data.Interfaces;
using BenevArts.Web.ViewModels.Home;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using static System.Net.Mime.MediaTypeNames;

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
		public async Task<IActionResult> Apply()
		{
			if (!await sellerService.CheckIfUserAppliedAsync(Guid.Parse(GetUserId())))
			{
				return View();
			}
			return RedirectToAction(nameof(StillReviewing));
		}

		[HttpGet]
		[Authorize(Roles = "User,Admin")]
		public IActionResult ThankYou()
		{
			return View();
		}

		[HttpGet]
		[Authorize(Roles = "User,Admin")]
		public IActionResult StillReviewing()
		{
			return View();
		}

		[HttpGet]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> AllApplications()
		{
			IEnumerable<SellerApplicationViewModel> models = await sellerService.GetAllApplicationsAsync();

			return View("~/Views/Seller/AllApplications.cshtml", models);
		}

		[HttpGet]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> SingleApplication(int id)
		{
			SellerApplicationViewModel model = await sellerService.GetSingleApplicationAsync(id);

			return View("~/Views/Seller/SingleApplication.cshtml", model);
		}

		[HttpGet]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> ApproveApplication(int id)
		{
			await sellerService.ApproveApplicationAsync(id);

			// TO DO: Send Notification to the user

			return RedirectToAction(nameof(AllApplications));
		}

		[HttpGet]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> DeclineApplication(int id)
		{
			await sellerService.DeclineApplicationAsync(id);

			// TO DO: Send Notification to the user

			return RedirectToAction(nameof(AllApplications));
		}

		// POST
		[HttpPost]
		[Authorize(Roles = "User,Admin")]
		public async Task<IActionResult> Apply(SellerApplicationViewModel application)
		{
			application.State = "Pending";
			if (ModelState.IsValid &&
				!await sellerService.CheckIfUserAppliedAsync(Guid.Parse(GetUserId())))
			{
				await sellerService.ApplyAsync(application, GetUserId());
				return RedirectToAction(nameof(ThankYou));
			}
			return View();
		}

	}
}
