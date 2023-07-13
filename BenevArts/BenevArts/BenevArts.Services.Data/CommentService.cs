﻿using AutoMapper;
using BenevArts.Data;
using BenevArts.Data.Models;
using BenevArts.Services.Data.Interfaces;
using BenevArts.Web.ViewModels.Home;
using Microsoft.EntityFrameworkCore;

namespace BenevArts.Services.Data
{
	public class CommentService : ICommentService
	{
		private readonly BenevArtsDbContext context;
		private readonly IMapper mapper;

		public CommentService(BenevArtsDbContext _context, IMapper _mapper)
		{
			context = _context;
			mapper = _mapper;
		}

		public async Task<CommentViewModel> AddCommentAsync(Guid assetId, string userId, string content)
		{
			ApplicationUser? user = await context.Users
				.Where(u => u.Id == Guid.Parse(userId))
				.FirstOrDefaultAsync();

			if (user == null)
			{
				throw new ArgumentException("Invalid User");
			}

			Comment comment = new Comment
			{
				Content = content,
				UserId = Guid.Parse(userId),
				PostedDate = DateTime.UtcNow,
				AssetId = assetId,
			};

			context.Comments.Add(comment);
			await context.SaveChangesAsync();

			CommentViewModel model = mapper.Map<CommentViewModel>(comment);
			return model;
		}
		public async Task RemoveCommentAsync(Guid assetId, string userId)
		{
			Comment? comment = context.Comments.FirstOrDefault(l => l.AssetId == assetId && l.UserId == Guid.Parse(userId));

			if (comment == null)
			{
				throw new ArgumentException("Comment not found");
			}

			context.Comments.Remove(comment);
			await context.SaveChangesAsync();
		}
	}
}
