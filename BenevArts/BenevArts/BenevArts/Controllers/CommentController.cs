﻿using BenevArts.Data.Models;
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
			CommentViewModel comment = await commentService.AddCommentAsync(assetId,GetUserId(),content);

			return PartialView("~/Views/Asset/_CommentItem.cshtml", comment);
		}

		[HttpPost]
		public async Task<IActionResult> RemoveComment(int commentId)
		{
			await commentService.RemoveCommentAsync(commentId, GetUserId());

            return Json(new { success = true});
        }
    }
}
