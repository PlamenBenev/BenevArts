using BenevArts.Data.Models;
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
    }
}
