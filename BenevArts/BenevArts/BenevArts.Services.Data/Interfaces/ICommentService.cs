﻿using BenevArts.Web.ViewModels.Home;

namespace BenevArts.Services.Data.Interfaces
{
	public interface ICommentService
	{
		Task AddCommentAsync(Guid assetId, string userId, string content);
		Task RemoveCommentAsync(Guid assetId, string userId);
	}
}
