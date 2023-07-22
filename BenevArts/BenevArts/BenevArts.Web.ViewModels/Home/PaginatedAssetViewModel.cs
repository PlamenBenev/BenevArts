using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenevArts.Web.ViewModels.Home
{
	public class PaginatedAssetViewModel
	{
		public IEnumerable<AssetViewModel> Assets { get; set; } = new List<AssetViewModel>();
		public int CurrentPage { get; set; }
		public int TotalItems { get; set; }
		public int ItemsPerPage { get; set; }
	}
}
