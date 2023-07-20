using BenevArts.Common.Exeptions;
using BenevArts.Data;
using BenevArts.Data.Models;
using BenevArts.Services.Data.Interfaces;
using BenevArts.Web.ViewModels.Home;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

namespace BenevArts.Services.Data
{
	public class SellerService : ISellerService
	{
		private readonly BenevArtsDbContext context;
		private readonly UserManager<ApplicationUser> userManager;

		public SellerService(BenevArtsDbContext _context,
			UserManager<ApplicationUser> _userManager)
		{
			context = _context;
			userManager = _userManager;
		}
		// GET
		public async Task<IEnumerable<SellerApplicationViewModel>> GetAllApplicationsAsync()
		{
			return await context.SellersApplications
				.Select(ap => new SellerApplicationViewModel
				{
					Id = ap.Id,
					Name = ap.Name,
					Email = ap.Email,
					Phone = ap.Phone,
					StoreDescription = ap.StoreDescription,
				})
				.ToListAsync();
		}
		public async Task<SellerApplicationViewModel> GetSingleApplicationAsync(int id)
		{
			return await context.SellersApplications
				.Where(ap => ap.Id == id)
				.Select(ap => new SellerApplicationViewModel
				{
					Id = ap.Id,
					Name = ap.Name,
					Email = ap.Email,
					Phone = ap.Phone,
					StoreDescription = ap.StoreDescription,
				}).FirstOrDefaultAsync() 
				?? throw new ArgumentNullException("Application Not Found!");
		}
		// POST
		public async Task ApplyAsync(SellerApplicationViewModel application, string userId)
		{
			Seller? seller = await context.Sellers.FindAsync(Guid.Parse(userId));

			SellerApplication? sellerApplication =
				await context.SellersApplications
				.Where(ap => ap.Id == application.Id).FirstOrDefaultAsync();

			ApplicationUser? user =
				await context.Users.FindAsync(Guid.Parse(userId))
				?? throw new UserNullException();

			if (sellerApplication == null && seller == null)
			{
				SellerApplication createApplication = new SellerApplication
				{
					Name = application.Name,
					Email = application.Email,
					Phone = application.Phone,
					StoreDescription = application.StoreDescription,
					ApplicationUserId = Guid.Parse(userId)
				};

				await context.SellersApplications.AddAsync(createApplication);
				await context.SaveChangesAsync();
			}
			else
			{
				throw new ArgumentNullException("Invalid attempt!");
			}

		}

		public async Task ApproveApplicationAsync(SellerApplicationViewModel application, string userId)
		{
			Seller? seller = await context.Sellers.FindAsync(Guid.Parse(userId));

			ApplicationUser? user =
					await context.Users.FindAsync(Guid.Parse(userId))
					?? throw new UserNullException();

			if (seller == null)
			{
				seller = new Seller
				{
					Id = Guid.Parse(userId),
					Name = application.Name,
					Email = application.Email,
				};

				await userManager.AddToRoleAsync(user, "Seller");

				await context.Sellers.AddAsync(seller);
				await context.SaveChangesAsync();
			}
			else
			{
				throw new ArgumentNullException("The Seller already exist!");
			}
		}

	}
}
