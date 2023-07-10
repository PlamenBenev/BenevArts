using AutoMapper;
using BenevArts.Data;
using BenevArts.Data.Models;
using BenevArts.Services.Data.Interfaces;
using BenevArts.Web.ViewModels.Home;

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

		public async Task<CommentViewModel> AddCommentAsync(Guid assetId,CommentViewModel commentModel)
		{
			ApplicationUser user = await context.Users.FindAsync(Guid.Parse(commentModel.User));

			Comment comment = new Comment
			{
				Content = commentModel.Content,
				User = user,
				PostedDate = DateTime.UtcNow,
				AssetId = commentModel.AssetId,
			};

			context.Comments.Add(comment);
			await context.SaveChangesAsync();

			CommentViewModel commentViewModel = mapper.Map<CommentViewModel>(comment);
			return commentViewModel;
		}
	}
}
