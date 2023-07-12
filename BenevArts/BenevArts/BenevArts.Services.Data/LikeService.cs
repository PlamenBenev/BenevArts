using BenevArts.Data;
using BenevArts.Data.Models;
using BenevArts.Services.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenevArts.Services.Data
{
	public class LikeService : ILikeService
	{
		private readonly BenevArtsDbContext context;

		public LikeService(BenevArtsDbContext _context)
		{
			context = _context;
		}

		public async Task AddLikeAsync(Guid assetId, string userId)
		{
			ApplicationUser? user = await context.Users
				.Where(u => u.Id == Guid.Parse(userId))
				.FirstOrDefaultAsync();

			if (user == null)
			{
				throw new ArgumentException("Invalid User Id");
			}

			Like like = new Like
			{
				AssetId = assetId,
				UserID = Guid.Parse(userId),
			};

			context.Likes.Add(like);
			await context.SaveChangesAsync();

		}

		public async Task RemoveLikeAsync(Guid assetId, string userId)
		{
			var like = context.Likes.FirstOrDefault(l => l.AssetId == assetId && l.UserID == Guid.Parse(userId));
			if (like != null)
			{
				context.Likes.Remove(like);
				await context.SaveChangesAsync();
			}
		}

		public async Task<int> GetLikeCountAsync(Guid assetId)
		{
			return await context.Likes
				.Where(l => l.AssetId == assetId)
				.CountAsync();
		}

		public async Task<bool> IsLikedByUserAsync(Guid assetId, string v)
		{
			var userLikes = await context.Likes
				.Where(l => l.AssetId == assetId && l.UserID == Guid.Parse(v))
				.ToListAsync();

			return userLikes.Any();
		}
	}
}
