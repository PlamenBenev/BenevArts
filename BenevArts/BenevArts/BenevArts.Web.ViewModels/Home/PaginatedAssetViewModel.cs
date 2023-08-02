
using System.ComponentModel.DataAnnotations;

namespace BenevArts.Web.ViewModels.Home
{
	public class PaginatedAssetViewModel
	{
		public IEnumerable<AssetViewModel> Items { get; set; } = new List<AssetViewModel>();
		public int CurrentPage { get; set; }
		public int TotalItems { get; set; }
		public int ItemsPerPage { get; set; }
		public int CategoryId { get; set; }

		[RegularExpression(@"^[A-Za-z0-9 ]+$", ErrorMessage = "Only letters, numbers, and spaces are allowed.")]
		public string? Query { get; set; }
	}
}
