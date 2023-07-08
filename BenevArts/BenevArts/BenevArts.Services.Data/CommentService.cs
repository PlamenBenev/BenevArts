using BenevArts.Data;
using BenevArts.Services.Data.Interfaces;
using BenevArts.Web.ViewModels.Home;
using Microsoft.AspNetCore.Mvc;

namespace BenevArts.Services.Data
{
	public class CommentService : ICommentService
	{
		private readonly BenevArtsDbContext context;

        public CommentService(BenevArtsDbContext _context)
        {
              context = _context;
        }

		// Get
		[HttpGet]
		public Task<IEnumerable<AssetViewModel>> GetCommentsAsync()
		{
			throw new NotImplementedException();
		}


	}
}
