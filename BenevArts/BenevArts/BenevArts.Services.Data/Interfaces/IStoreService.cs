using BenevArts.Web.ViewModels.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenevArts.Services.Data.Interfaces
{
	public interface IStoreService
	{
		// Get
		Task<IEnumerable<AssetViewModel>> GetMyStoreAsync(string userId);
		Task<EditAssetViewModel> GetEditByIdAsync(Guid id, string userId);

		// Post
		Task AddAssetAsync(AddAssetViewModel model, string userId);
		Task RemoveAssetAsync(Guid assetId, string userId);
		Task EditAssetAsync(EditAssetViewModel model, string userId);
	}
}
