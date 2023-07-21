using BenevArts.Web.ViewModels.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenevArts.Services.Data.Interfaces
{
	public interface ISellerService
	{
		// GET
        Task<bool> CheckIfUserAppliedAsync(Guid appliedUserId);
        Task<IEnumerable<SellerApplicationViewModel>> GetAllApplicationsAsync();
		Task<SellerApplicationViewModel> GetSingleApplicationAsync(int id);
		Task ApproveApplicationAsync(int id);

		// POST
		Task ApplyAsync(SellerApplicationViewModel application, string userId);
		Task DeclineApplicationAsync(int id);
	}
}
