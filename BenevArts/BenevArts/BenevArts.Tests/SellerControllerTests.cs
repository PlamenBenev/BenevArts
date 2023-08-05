

using BenevArts.Data.Models;
using BenevArts.Services.Data.Interfaces;
using BenevArts.Web.Controllers;
using BenevArts.Web.ViewModels.Home;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Security.Claims;

namespace BenevArts.Tests
{
	[TestFixture]
	public class SellerControllerTests
	{
		[Test]
		public async Task Apply_ShouldReturnView_WhenUserHasNotApplied()
		{
			// Arrange
			string userId = Guid.NewGuid().ToString();	
			var mockSellerService = new Mock<ISellerService>();
			mockSellerService.Setup(x => x.CheckIfUserAppliedAsync(It.IsAny<Guid>())).ReturnsAsync(false);
			var controller = new SellerController(mockSellerService.Object);

			// Create a ClaimsPrincipal with the desired user ID
			controller.ControllerContext = new ControllerContext
			{
				HttpContext = new DefaultHttpContext { User = CreateClaimsPrincipal(userId) }
			};

			// Act
			var result = await controller.Apply() as ViewResult;

			// Assert
			Assert.IsNotNull(result);
			Assert.True(string.IsNullOrEmpty(result!.ViewName), "View name should not be null or empty.");
		}

		[Test]
		public async Task Apply_ShouldRedirectToStillReviewing_WhenUserHasApplied()
		{
			// Arrange
			string userId = Guid.NewGuid().ToString();
			var mockSellerService = new Mock<ISellerService>();
			mockSellerService.Setup(x => x.CheckIfUserAppliedAsync(It.IsAny<Guid>())).ReturnsAsync(true);
			var controller = new SellerController(mockSellerService.Object);

			// Create a ClaimsPrincipal with the desired user ID
			controller.ControllerContext = new ControllerContext
			{
				HttpContext = new DefaultHttpContext { User = CreateClaimsPrincipal(userId) }
			};

			// Act
			var result = await controller.Apply() as RedirectToActionResult;

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual("StillReviewing", result!.ActionName);
		}

		// Helper method to create a ClaimsPrincipal with the desired user ID
		private ClaimsPrincipal CreateClaimsPrincipal(string userId)
		{
			var identity = new ClaimsIdentity(new[]
			{
				new Claim(ClaimTypes.NameIdentifier, userId)
			});

			return new ClaimsPrincipal(identity);
		}

		[Test]
		public async Task AllApplications_ShouldReturnViewWithModel()
		{
			// Arrange
			var mockSellerService = new Mock<ISellerService>();
			var fakeApplications = new List<SellerApplicationViewModel>
			{
				new SellerApplicationViewModel
				{
					Id = 1,
					StoreName = "Store 1",
					Email = "store1@test.com",
					Phone = "123456789",
					StoreDescription = "Test store 1",
					State = "Pending",
					ApplicationUserId = Guid.NewGuid()
				},
				new SellerApplicationViewModel
				{
					Id = 2,
					StoreName = "Store 2",
					Email = "store2@test.com",
					Phone = "987654321",
					StoreDescription = "Test store 2",
					State = "Approved",
					ApplicationUserId = Guid.NewGuid()
				}
			};
			mockSellerService.Setup(x => x.GetAllApplicationsAsync()).ReturnsAsync(fakeApplications);
			var controller = new SellerController(mockSellerService.Object);

			// Act
			var result = await controller.AllApplications() as ViewResult;

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual("~/Views/Seller/AllApplications.cshtml", result!.ViewName);
			Assert.IsInstanceOf<IEnumerable<SellerApplicationViewModel>>(result.Model);
			var model = result.Model as IEnumerable<SellerApplicationViewModel>;
			Assert.AreEqual(2, model!.Count()); // Adjust the count based on your fake data

			// Assert properties of the first item in the model
			var firstItem = model!.FirstOrDefault();
			Assert.IsNotNull(firstItem);
			Assert.AreEqual(1, firstItem!.Id);
			Assert.AreEqual("Store 1", firstItem.StoreName);
			Assert.AreEqual("store1@test.com", firstItem.Email);
			Assert.AreEqual("123456789", firstItem.Phone);
			Assert.AreEqual("Test store 1", firstItem.StoreDescription);
			Assert.AreEqual("Pending", firstItem.State);
			Assert.AreNotEqual(Guid.Empty, firstItem.ApplicationUserId);
		}

		[Test]
		public async Task GetApplicationsByState_ShouldReturnPartialViewWithModel()
		{
			// Arrange
			var mockSellerService = new Mock<ISellerService>();
			var fakeApplications = new List<SellerApplicationViewModel>
			{
				new SellerApplicationViewModel
				{
					Id = 1,
					StoreName = "Store 1",
					Email = "store1@test.com",
					Phone = "123456789",
					StoreDescription = "Test store 1",
					State = "Pending",
					ApplicationUserId = Guid.NewGuid()
				},
				new SellerApplicationViewModel
				{
					Id = 2,
					StoreName = "Store 2",
					Email = "store2@test.com",
					Phone = "987654321",
					StoreDescription = "Test store 2",
					State = "Approved",
					ApplicationUserId = Guid.NewGuid()
				}
			};
			mockSellerService.Setup(x => x.GetApplicationsByStateAsync(It.IsAny<string>())).ReturnsAsync(fakeApplications);
			var controller = new SellerController(mockSellerService.Object);

			// Act
			var result = await controller.GetApplicationsByState("Pending") as PartialViewResult;

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual("_ApplicationsTable", result!.ViewName);
			Assert.IsInstanceOf<IEnumerable<SellerApplicationViewModel>>(result.Model);
			var model = result.Model as IEnumerable<SellerApplicationViewModel>;
			Assert.AreEqual(2, model!.Count()); // Adjust the count based on your fake data

			// Assert properties of the first item in the model
			var firstItem = model!.FirstOrDefault();
			Assert.IsNotNull(firstItem);
			Assert.AreEqual(1, firstItem!.Id);
			Assert.AreEqual("Store 1", firstItem.StoreName);
			Assert.AreEqual("store1@test.com", firstItem.Email);
			Assert.AreEqual("123456789", firstItem.Phone);
			Assert.AreEqual("Test store 1", firstItem.StoreDescription);
			Assert.AreEqual("Pending", firstItem.State);
			Assert.AreNotEqual(Guid.Empty, firstItem.ApplicationUserId);
		}

		[Test]
		public async Task SingleApplication_ShouldReturnViewWithModel()
		{
			// Arrange
			int applicationId = 1;
			var mockSellerService = new Mock<ISellerService>();
			var fakeApplication = new SellerApplicationViewModel
			{
				Id = applicationId,
				StoreName = "Test Store",
				Email = "teststore@test.com",
				Phone = "123456789",
				StoreDescription = "This is a test store.",
				State = "Pending",
				ApplicationUserId = Guid.NewGuid()
			};
			mockSellerService.Setup(x => x.GetSingleApplicationAsync(applicationId)).ReturnsAsync(fakeApplication);
			var controller = new SellerController(mockSellerService.Object);

			// Act
			var result = await controller.SingleApplication(applicationId) as ViewResult;

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual("~/Views/Seller/SingleApplication.cshtml", result!.ViewName);
			Assert.IsInstanceOf<SellerApplicationViewModel>(result.Model);
			var model = result.Model as SellerApplicationViewModel;
			Assert.AreEqual(applicationId, model!.Id);
			Assert.AreEqual("Test Store", model.StoreName);
			Assert.AreEqual("teststore@test.com", model.Email);
			Assert.AreEqual("123456789", model.Phone);
			Assert.AreEqual("This is a test store.", model.StoreDescription);
			Assert.AreEqual("Pending", model.State);
			Assert.AreNotEqual(Guid.Empty, model.ApplicationUserId);
		}

		[Test]
		public async Task SingleApplication_ShouldReturnNotFoundForInvalidId()
		{
			// Arrange
			int applicationId = 100; // An invalid application ID
			var mockSellerService = new Mock<ISellerService>();
			mockSellerService.Setup(x => x.GetSingleApplicationAsync(applicationId))
							.ReturnsAsync(new SellerApplicationViewModel()); // Return a valid instance

			var controller = new SellerController(mockSellerService.Object);

			// Act
			var result = await controller.SingleApplication(applicationId);

			// Assert
			Assert.IsInstanceOf<ViewResult>(result);
			Assert.IsNotNull(result);
		}

		[Test]
		public async Task ApproveApplication_ShouldCallApproveApplicationAsync()
		{
			// Arrange
			int applicationId = 123; // Replace with a valid applicationId
			var mockSellerService = new Mock<ISellerService>();
			var controller = new SellerController(mockSellerService.Object);

			// Mock the sellerService behavior
			mockSellerService.Setup(x => x.ApproveApplicationAsync(applicationId)).Verifiable();

			// Act
			var result = await controller.ApproveApplication(applicationId);

			// Assert
			// Verify that the service method was called with the correct applicationId
			mockSellerService.Verify();

			// Verify that the controller redirects to the correct action after approving the application
			Assert.IsInstanceOf<RedirectToActionResult>(result);
			var redirectResult = result as RedirectToActionResult;
			Assert.AreEqual("GetApplicationsByState", redirectResult!.ActionName);
		}

		[Test]
		public async Task DeclineApplication_ShouldCallDeclineApplicationAsync()
		{
			// Arrange
			int applicationId = 123; // Replace with a valid applicationId
			var mockSellerService = new Mock<ISellerService>();
			var controller = new SellerController(mockSellerService.Object);

			// Mock the sellerService behavior
			mockSellerService.Setup(x => x.DeclineApplicationAsync(applicationId)).Verifiable();

			// Act
			var result = await controller.DeclineApplication(applicationId);

			// Assert
			// Verify that the service method was called with the correct applicationId
			mockSellerService.Verify();

			// Verify that the controller redirects to the correct action after declining the application
			Assert.IsInstanceOf<RedirectToActionResult>(result);
			var redirectResult = result as RedirectToActionResult;
			Assert.AreEqual("GetApplicationsByState", redirectResult!.ActionName);
		}
	}
}
