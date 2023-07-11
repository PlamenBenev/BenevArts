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
		public async Task<IActionResult> PostComment(Guid assetId,CommentViewModel comment)
		{
			comment.User = GetUserId();
			if (ModelState.IsValid)
			{
				await commentService.AddCommentAsync(assetId,comment);

				return RedirectToAction("Details", "Asset", new { id = assetId });
			}

			return View();
		}
	}
}
