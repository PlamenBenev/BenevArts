using BenevArts.Common;
using BenevArts.Services.Data.Interfaces;
using BenevArts.Web.ViewModels.Home;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BenevArts.Web.Controllers
{
	public class CommentController : BaseController
	{
		private readonly ICommentService commentService;
		private readonly ILogger<CommentController> logger;

		public CommentController(ICommentService _commentService, ILogger<CommentController> _logger)
		{
			commentService = _commentService;
			logger = _logger;
		}

		// Posting a comment will be availible only for users who purchased the asset
		// Post
		[HttpPost]
		[Authorize(Roles = "User,Seller,Admin")]
		public async Task<IActionResult> PostComment(Guid assetId, string content)
		{
			try
			{
				if (!Validations.IsValidQuery(content))
				{
					return View();
				}

				CommentViewModel comment = await commentService.AddCommentAsync(assetId, GetUserId(), content);

				return PartialView("~/Views/Asset/_CommentItem.cshtml", comment);
			}
			catch (Exception ex)
			{
				logger.LogError(ex, "An exception occurred in the PostComment action.");
				return View("Error");
			}
		}

		[HttpPost]
		[Authorize(Roles = "User,Seller,Admin")]
		public async Task<IActionResult> RemoveComment(int commentId)
		{
			try
			{
				await commentService.RemoveCommentAsync(commentId, GetUserId());

				return Json(new { success = true });
			}
			catch (Exception ex)
			{
				logger.LogError(ex, "An exception occurred in the RemoveComment action.");
				return View("Error");
			}
		}
	}
}
