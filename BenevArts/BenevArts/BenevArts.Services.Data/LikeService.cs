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
			ApplicationUser user = await context.Users
				.Where(u => u.Id == Guid.Parse(userId))
				.FirstOrDefaultAsync()
				?? throw new ArgumentNullException("Invalid User Id.");

			Asset asset = await context.Assets.Where(a => a.Id == assetId).FirstOrDefaultAsync()
				?? throw new InvalidOperationException("Invalid asset id");

			Like like = new Like
			{
				AssetId = assetId,
				UserId = Guid.Parse(userId),
			};

			context.Likes.Add(like);
			await context.SaveChangesAsync();

		}

		public async Task RemoveLikeAsync(Guid assetId, string userId)
		{
			Like like = await GetLikeByUserAsync(assetId, userId)
				?? throw new ArgumentNullException("Like not found");

			context.Likes.Remove(like);
			await context.SaveChangesAsync();
		}

		public async Task<int> GetLikeCountAsync(Guid assetId)
		{
			return await context.Likes
				.Where(l => l.AssetId == assetId)
				.CountAsync();
		}

		public async Task<bool> IsLikedByUserAsync(Guid assetId, string userId)
		{
			bool userLike = await context.Likes
				.Where(l => l.AssetId == assetId && l.UserId == Guid.Parse(userId))
				.FirstOrDefaultAsync() != null;

			return userLike;
		}

		public async Task<Like> GetLikeByUserAsync(Guid assetId, string userId)
		{
			Like userLike = await context.Likes
				.Where(l => l.AssetId == assetId && l.UserId == Guid.Parse(userId))
				.FirstOrDefaultAsync()
				?? throw new ArgumentNullException("Like not found");

			return userLike;
		}
	}
}
