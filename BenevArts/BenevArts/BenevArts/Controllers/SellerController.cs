using BenevArts.Services.Data.Interfaces;
using BenevArts.Web.Infrastructure;
using BenevArts.Web.ViewModels.Home;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


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
		public async Task<IActionResult> GetApplicationsByState(string state)
		{
			IEnumerable<SellerApplicationViewModel> applications;

			// Retrieve applications based on the selected state
			if (string.IsNullOrEmpty(state))
			{
				applications = await sellerService.GetAllApplicationsAsync();

			}
			else
			{
				applications = await sellerService.GetApplicationsByStateAsync(state);

			}

			// Return a partial view with the filtered applications data
			return PartialView("_ApplicationsTable", applications);
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

			return RedirectToAction(nameof(GetApplicationsByState));
		}

		// POST
		[HttpPost]
		[Authorize(Roles = "User,Admin")]
		public async Task<IActionResult> Apply(SellerApplicationViewModel application)
		{
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
