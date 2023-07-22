using BenevArts.Web.ViewModels.Home;

namespace BenevArts.Web.Infrastructure
{
    public static class Pagination
    {
        public static PaginatedAssetViewModel Paginator(IEnumerable<AssetViewModel> models, int page, int itemsPerPage)
        {
            int totalItems = models.Count();

            // Calculate the number of items to skip based on the current page and items per page
            int skip = (page - 1) * itemsPerPage;

            // Get the items for the current page using Skip and Take LINQ methods
            var itemsForPage = models.Skip(skip).Take(itemsPerPage).ToList();

            // Create the paginated view model for the current page of items
            var paginatedViewModel = new PaginatedAssetViewModel
            {
                Items = itemsForPage,
                CurrentPage = page,
                ItemsPerPage = itemsPerPage,
                TotalItems = totalItems
            };

            return paginatedViewModel;
        }

    }
}
