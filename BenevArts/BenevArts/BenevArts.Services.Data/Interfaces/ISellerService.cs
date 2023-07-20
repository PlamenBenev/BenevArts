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
		Task ApplyAsync(SellerApplicationViewModel application, string userId);
		Task ApproveApplicationAsync(SellerApplicationViewModel application, string userId);
		Task<IEnumerable<SellerApplicationViewModel>> GetAllApplicationsAsync();
		Task<SellerApplicationViewModel> GetSingleApplicationAsync(int id);
	}
}
