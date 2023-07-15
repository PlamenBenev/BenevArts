﻿using BenevArts.Data;
using BenevArts.Data.Models;
using BenevArts.Services.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BenevArts.Services.Data
{
	public class FavoriteService : IFavoriteService
	{
		private readonly BenevArtsDbContext context;

		public FavoriteService(BenevArtsDbContext _context)
		{
			context = _context;
		}

		public async Task AddToFavoriteAsync(Guid assetId, string userId)
		{
			ApplicationUser user = await context.Users
							.Where(u => u.Id == Guid.Parse(userId))
							.FirstOrDefaultAsync()
							?? throw new ArgumentNullException("Invalid User Id.");

			Asset asset = await context.Assets.Where(a => a.Id == assetId).FirstOrDefaultAsync()
				?? throw new InvalidOperationException("Invalid asset id");

			UserFavorites favorite = new UserFavorites
			{
				AssetId = assetId,
				UserId = Guid.Parse(userId),
			};

			context.UserFavorites.Add(favorite);
			await context.SaveChangesAsync();
		}

		public async Task RemoveFromFavoriteAsync(Guid assetId, string userId)
		{
			UserFavorites favorite = await GetFavoriteByUserAsync(assetId, userId)
			?? throw new ArgumentNullException("Favorite Asset not found");

			context.UserFavorites.Remove(favorite);
			await context.SaveChangesAsync();
		}
		public async Task<bool> IsFavoritedByUserAsync(Guid assetId, string userId)
		{
			bool userFavorite = await context.UserFavorites
				.Where(l => l.AssetId == assetId && l.UserId == Guid.Parse(userId))
				.FirstOrDefaultAsync() != null;

			return userFavorite;
		}
		public async Task<UserFavorites> GetFavoriteByUserAsync(Guid assetId, string userId)
		{
			UserFavorites userFavorite = await context.UserFavorites
				.Where(l => l.AssetId == assetId && l.UserId == Guid.Parse(userId))
				.FirstOrDefaultAsync()
				?? throw new ArgumentNullException("Favorite Asset not found");

			return userFavorite;
		}
	}
}
