using BenevArts.Common;
using BenevArts.Services.Data.Interfaces;
using BenevArts.Web.ViewModels.Home;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;


namespace BenevArts.Web.Controllers
{
	public class SellerController : BaseController
	{
		private readonly ISellerService sellerService;
		private readonly ILogger<SellerController> logger;

		public SellerController(ISellerService _sellerService, ILogger<SellerController> _logger)
		{
			sellerService = _sellerService;
			logger = _logger;
		}

		// GET
		[HttpGet]
		[Authorize(Roles = "User,Admin")]
		public async Task<IActionResult> Apply()
		{
			try
			{
				if (!await sellerService.CheckIfUserAppliedAsync(Guid.Parse(GetUserId())))
				{
					return View();
				}
				return RedirectToAction(nameof(StillReviewing));
			}
			catch (Exception ex)
			{
				logger.LogError(ex, "An exception occurred in the Apply action.");
				return View("Error");
			}
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
			try
			{
				IEnumerable<SellerApplicationViewModel> models = await sellerService.GetAllApplicationsAsync();

				return View("~/Views/Seller/AllApplications.cshtml", models);
			}
			catch (Exception ex)
			{
				logger.LogError(ex, "An exception occurred in the AllApplications action.");
				return View("Error");
			}
		}

		[HttpGet]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> GetApplicationsByState(string state)
		{
			try
			{
				IEnumerable<SellerApplicationViewModel> applications;

				// Retrieve applications based on the selected state
				if (string.IsNullOrEmpty(state) || !Validations.IsValidQuery(state))
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
			catch (Exception ex)
			{
				logger.LogError(ex, "An exception occurred in the GetApplicationByState action.");
				return View("Error");
			}
		}

		[HttpGet]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> SingleApplication(int id)
		{
			try
			{
				SellerApplicationViewModel model = await sellerService.GetSingleApplicationAsync(id);

				return View("~/Views/Seller/SingleApplication.cshtml", model);
			}
			catch (Exception ex)
			{
				logger.LogError(ex, "An exception occurred in the SingleApplication action.");
				return View("Error");
			}
		}

		[HttpGet]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> ApproveApplication(int id)
		{
			try
			{
				await sellerService.ApproveApplicationAsync(id);

				// TO DO: Send Notification to the user

				return RedirectToAction(nameof(AllApplications));
			}
			catch (Exception ex)
			{
				logger.LogError(ex, "An exception occurred in the ApproveApplication action.");
				return View("Error");
			}
		}

		[HttpGet]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> DeclineApplication(int id)
		{
			try
			{
				await sellerService.DeclineApplicationAsync(id);

				// TO DO: Send Notification to the user

				return RedirectToAction(nameof(GetApplicationsByState));
			}
			catch (Exception ex)
			{
				logger.LogError(ex, "An exception occurred in the DeclineApplication action.");
				return View("Error");
			}
		}

		// POST
		[HttpPost]
		[Authorize(Roles = "User,Admin")]
		public async Task<IActionResult> Apply(SellerApplicationViewModel application)
		{
			try
			{
				if (ModelState.IsValid &&
					!await sellerService.CheckIfUserAppliedAsync(Guid.Parse(GetUserId())))
				{
					await sellerService.ApplyAsync(application, GetUserId());
					return RedirectToAction(nameof(ThankYou));
				}
				return View();
			}
			catch (Exception ex)
			{
				logger.LogError(ex, "An exception occurred in the Details action.");
				return View("Error");
			}
		}
	}
}
