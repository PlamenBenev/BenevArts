using BenevArts.Services.Data.Interfaces;
using BenevArts.Web.Controllers;
using BenevArts.Web.ViewModels.Home;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
			var mockLogger = new Mock<ILogger<CommentController>>();

			var controller = new CommentController(mockCommentService.Object, mockLogger.Object);

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

	
	}
}
