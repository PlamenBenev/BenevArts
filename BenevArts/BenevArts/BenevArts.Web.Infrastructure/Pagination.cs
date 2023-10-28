using BenevArts.Web.ViewModels.Home;

namespace BenevArts.Web.Infrastructure
{
	public static class Pagination
	{
		public static PaginatedAssetViewModel Paginator(IEnumerable<AssetViewModel> models,
			string? query, int categoryId, int page, int itemsPerPage, string? currentSortOrder)
		{
			int totalItems = models.Count();

			// Calculate the number of items to skip based on the current page and items per page
			int skip = (page - 1) * itemsPerPage;

			// Apply the sorting order based on the currentSortOrder parameter
			var orderedModels = ApplySorting(models, currentSortOrder);

			// Get the items for the current page using Skip and Take LINQ methods
			var itemsForPage = orderedModels.Skip(skip).Take(itemsPerPage).ToList();

			// Create the paginated view model for the current page of items
			var paginatedViewModel = new PaginatedAssetViewModel
			{
				Items = itemsForPage,
				CurrentPage = page,
				ItemsPerPage = itemsPerPage,
				TotalItems = totalItems,
				Query = query,
				CategoryId = categoryId,
				CurrentSortOrder = currentSortOrder // Store the currentSortOrder in the ViewModel
			};

			return paginatedViewModel;
		}

		private static IEnumerable<AssetViewModel> ApplySorting(
			IEnumerable<AssetViewModel> models,
			string? currentSortOrder)
		{
			if (!string.IsNullOrEmpty(currentSortOrder))
			{
				switch (currentSortOrder)
				{
					case "price":
						models = models.OrderBy(asset => asset.Price);
						break;
					case "title":
						models = models.OrderBy(asset => asset.Title);
						break;
					case "uploadDate":
						models = models.OrderByDescending(asset => asset.UploadDate);
						break;
					default:
						break;
				}
			}

			return models;
		}
	}
}
