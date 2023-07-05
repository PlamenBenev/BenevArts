using BenevArts.Data.Models;
using BenevArts.Web.ViewModels.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenevArts.Services.Data.Interfaces
{
    public interface IAssetService
    {
        Task<IEnumerable<Category>> GetCategoriesAsync();
        Task AddAssetAsync(AddAssetViewModel model);
        Task<AssetViewModel> GetAssetByIdAsync(string id);
    }
}
