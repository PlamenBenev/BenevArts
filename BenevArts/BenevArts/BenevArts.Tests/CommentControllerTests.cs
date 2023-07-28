using BenevArts.Services.Data.Interfaces;
using BenevArts.Web.Controllers;
using BenevArts.Web.ViewModels.Home;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Security.Claims;

namespace BenevArts.Tests
{
	[TestFixture]
	public class CommentControllerTests
	{
		[Test]
		public async Task PostComment_ShouldReturnPartialViewWithComment()
		{
			// Arrange
			var userId = "sampleUserId";
			var assetId = Guid.NewGuid();
			var content = "Test comment content";
			var mockCommentService = new Mock<ICommentService>();
			var controller = new CommentController(mockCommentService.Object);

			// Setup the behavior of AddCommentAsync() to return mock data
			var mockComment = new CommentViewModel
			{
				Id = 1,
				Content = content,
				PostedDate = DateTime.UtcNow,
				User = "TestUser",
				AssetId = assetId,
				UserId = Guid.NewGuid()
			};
			mockCommentService.Setup(service => service.AddCommentAsync(assetId, It.IsAny<string>(), content))
							  .ReturnsAsync(mockComment);

			// Mock the HttpContext and User to represent an authenticated user
			var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
			{
				new Claim(ClaimTypes.NameIdentifier, userId),
			}));

			controller.ControllerContext = new ControllerContext
			{
				HttpContext = new DefaultHttpContext { User = user }
			};

			// Act
			var result = await controller.PostComment(assetId, content) as PartialViewResult;

			// Assert
			Assert.NotNull(result); // Check if the result is not null
			Assert.AreEqual("~/Views/Asset/_CommentItem.cshtml", result!.ViewName); // Check if the correct partial view is returned

			// Verify that the correct data is passed to the view model
			var viewModel = result.Model as CommentViewModel;
			Assert.NotNull(viewModel);
			Assert.AreEqual(mockComment.Id, viewModel!.Id);
			Assert.AreEqual(mockComment.Content, viewModel.Content);
			Assert.AreEqual(mockComment.PostedDate, viewModel.PostedDate);
			Assert.AreEqual(mockComment.User, viewModel.User);
			Assert.AreEqual(mockComment.AssetId, viewModel.AssetId);
			Assert.AreEqual(mockComment.UserId, viewModel.UserId);

			// You can add more assertions here to check the data in the view model if needed
		}

		[Test]
		public async Task RemoveComment_ShouldReturnJsonSuccess()
		{
			// Arrange
			int commentId = 123;
			var userId = "your-user-id"; // Set your user ID here
			var mockCommentService = new Mock<ICommentService>();
			var controller = new CommentController(mockCommentService.Object);

			// Create a ClaimsPrincipal with the desired user ID
			var claims = new[] { new Claim(ClaimTypes.NameIdentifier, userId) };
			var identity = new ClaimsIdentity(claims);
			var user = new ClaimsPrincipal(identity);
			controller.ControllerContext = new ControllerContext
			{
				HttpContext = new DefaultHttpContext { User = user }
			};

			// Act
			var result = await controller.RemoveComment(commentId) as JsonResult;

			// Assert
			Assert.IsNotNull(result);
			var successValue = result.Value.GetType().GetProperty("Success")?.GetValue(result.Value);
			Assert.AreEqual(true, successValue);
			// Add any other assertions as needed
		}

		[Test]
		public async Task RemoveComment_ShouldCallRemoveCommentAsync()
		{
			// Arrange
			var commentId = 123;
			var userId = "your-user-id"; // Set your user ID here
			var mockCommentService = new Mock<ICommentService>();
			var controller = new CommentController(mockCommentService.Object);

			// Mock the User property in HttpContext
			var mockUser = new Mock<ClaimsPrincipal>();
			mockUser.Setup(u => u.FindFirstValue(It.IsAny<string>())).Returns(userId);
			controller.ControllerContext = new ControllerContext
			{
				HttpContext = new DefaultHttpContext
				{
					User = mockUser.Object
				}
			};

			// Act
			var result = await controller.RemoveComment(commentId) as JsonResult;

			// Assert
			Assert.IsNotNull(result);
			var successValue = result.Value.GetType().GetProperty("success")?.GetValue(result.Value);
			Assert.AreEqual(true, successValue);
			// Add any other assertions as needed
		}
		[Test]
		public async Task RemoveComment_ShouldReturnJsonSuccessOnSuccess()
		{
			// Arrange
			var commentId = 1;
			var userId = Guid.NewGuid().ToString();
			var mockCommentService = new Mock<ICommentService>();
			mockCommentService.Setup(service => service.RemoveCommentAsync(commentId, userId))
							  .Returns(Task.CompletedTask); // Simulate successful removal
			var controller = new CommentController(mockCommentService.Object);

			// Mock the HttpContext and User to represent an authenticated user
			var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
			{
				new Claim(ClaimTypes.NameIdentifier, userId),
			}));

			controller.ControllerContext = new ControllerContext
			{
				HttpContext = new DefaultHttpContext { User = user }
			};

			// Act
			var result = await controller.RemoveComment(commentId) as JsonResult;

			// Assert
			Assert.NotNull(result); // Check if the result is not null

			// Verify that the result is a JSON object with the "success" property set to true
			var jsonResultData = result!.Value as dynamic;
			Assert.NotNull(jsonResultData);
			Assert.IsTrue(jsonResultData!.success);
		}

		[Test]
		public async Task RemoveComment_ShouldReturnBadRequestOnError()
		{
			// Arrange
			var commentId = 1;
			var userId = Guid.NewGuid().ToString();
			var mockCommentService = new Mock<ICommentService>();
			mockCommentService.Setup(service => service.RemoveCommentAsync(commentId, userId))
							  .Throws(new ArgumentNullException("Comment not found")); // Simulate an error
			var controller = new CommentController(mockCommentService.Object);

			// Mock the HttpContext and User to represent an authenticated user
			var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
			{
				new Claim(ClaimTypes.NameIdentifier, userId),
			}));

			controller.ControllerContext = new ControllerContext
			{
				HttpContext = new DefaultHttpContext { User = user }
			};
			// Act
			var result = await controller.RemoveComment(commentId) as BadRequestResult;

			// Assert
			Assert.NotNull(result); // Check if the result is not null
		}
	}
}
