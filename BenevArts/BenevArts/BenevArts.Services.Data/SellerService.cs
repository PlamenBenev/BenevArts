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
					Name = ap.StoreName,
					Email = ap.StoreEmail,
					Phone = ap.StorePhone,
					StoreDescription = ap.StoreDescription,
					State = ap.State,
				})
				.ToListAsync();
		}
		public async Task<IEnumerable<SellerApplicationViewModel>> GetApplicationsByStateAsync(string state)
		{
			return await context.SellersApplications
				.Where(ap => ap.State == state)
				.Select(ap => new SellerApplicationViewModel
				{
					Id = ap.Id,
					Name = ap.StoreName,
					Email = ap.StoreEmail,
					Phone = ap.StorePhone,
					StoreDescription = ap.StoreDescription,
					State = ap.State,
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
					Name = ap.StoreName,
					Email = ap.StoreEmail,
					Phone = ap.StorePhone,
					StoreDescription = ap.StoreDescription,
					State = ap.State,
				}).FirstOrDefaultAsync()
				?? throw new ArgumentNullException("Application Not Found!");
		}
		public async Task<bool> CheckIfUserAppliedAsync(Guid userId)
		{
			SellerApplication? sellerApplication = await context.SellersApplications
				.Where(ap => ap.ApplicationUserId == userId && ap.State != "Declined")
				.FirstOrDefaultAsync();

			if (sellerApplication == null)
			{
				return false;
			}
			return true;
		}

		// POST
		public async Task ApplyAsync(SellerApplicationViewModel application, string userId)
		{
			Seller? seller = await context.Sellers.FindAsync(Guid.Parse(userId));

			if (!await CheckIfUserAppliedAsync(application.ApplicationUserId) && seller == null)
			{
				SellerApplication createApplication = new SellerApplication
				{
					StoreName = application.Name,
					StoreEmail = application.Email,
					StorePhone = application.Phone,
					StoreDescription = application.StoreDescription,
					ApplicationUserId = Guid.Parse(userId)
				};

				createApplication.State = "Pending";

				await context.SellersApplications.AddAsync(createApplication);
				await context.SaveChangesAsync();
			}
		}

		public async Task ApproveApplicationAsync(int id)
		{
			// Check if the application exist
			SellerApplication application = await context.SellersApplications.FindAsync(id)
				?? throw new ArgumentNullException("Application does not exist!");

			// Check if the user exist
			ApplicationUser user =
				await context.Users.FindAsync(application.ApplicationUserId)
				?? throw new UserNullException();

			// Check if the seller already exist
			Seller? seller = await context.Sellers.FindAsync(application.ApplicationUserId);

			if (seller == null)
			{
				seller = new Seller
				{
					Id = application.ApplicationUserId,
					Name = application.StoreName,
					Email = application.StoreEmail,
				};

				application.State = "Approved";
				await userManager.AddToRoleAsync(user, "Seller");
				await userManager.RemoveFromRoleAsync(user, "User");

				await context.Sellers.AddAsync(seller);
				await context.SaveChangesAsync();
			}
		}

		public async Task DeclineApplicationAsync(int id)
		{
			// Check if the application exist
			SellerApplication application = await context.SellersApplications.FindAsync(id)
				?? throw new ArgumentNullException("Application does not exist!");

			// Check if the user exist
			ApplicationUser user =
				await context.Users.FindAsync(application.ApplicationUserId)
				?? throw new UserNullException();

			// Check if the seller already exist
			Seller? seller = await context.Sellers.FindAsync(application.ApplicationUserId);

			if (seller != null)
			{
				throw new ArgumentNullException("The Seller already exist!");
			}

			application.State = "Declined";

			await context.SaveChangesAsync();
		}

	}

}

