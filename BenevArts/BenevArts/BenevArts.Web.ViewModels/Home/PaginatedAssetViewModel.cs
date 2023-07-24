
namespace BenevArts.Web.ViewModels.Home
{
	public class PaginatedAssetViewModel
	{
		public IEnumerable<AssetViewModel> Items { get; set; } = new List<AssetViewModel>();
		public int CurrentPage { get; set; }
		public int TotalItems { get; set; }
		public int ItemsPerPage { get; set; }
		public int CategoryId { get; set; }
		public string? Query { get; set; }
	}
}
