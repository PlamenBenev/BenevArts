using BenevArts.Services.Data.Interfaces;
using BenevArts.Web.ViewModels.Home;
using Microsoft.AspNetCore.Mvc;

namespace BenevArts.Web.Controllers
{
	public class CommentController : BaseController
	{
		private readonly ICommentService commentService;

		public CommentController(ICommentService _commentService)
		{
			commentService = _commentService;
		}
		// Post

		[HttpPost]
		public async Task<IActionResult> PostComment(Guid assetId, string content)
		{

			await commentService.AddCommentAsync(assetId,GetUserId(),content);
			return Ok(content);
		}

		[HttpPost]
		public async Task<IActionResult> RemoveComment(Guid assetId)
		{
			await commentService.RemoveCommentAsync(assetId, GetUserId());

			return RedirectToAction("Details", "Asset", new { id = assetId });
		}
	}
}
