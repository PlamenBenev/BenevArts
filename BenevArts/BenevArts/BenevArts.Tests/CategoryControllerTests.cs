using BenevArts.Controllers;
using BenevArts.Services.Data.Interfaces;
using BenevArts.Web.Controllers;
using BenevArts.Web.ViewModels.Home;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace BenevArts.Tests
{
	[TestFixture]
	public class CategoryControllerTests
	{
		[Test]
		public async Task Index_ShouldReturnCorrectViewResultWithCategories()
		{
			// Arrange
			var mockCategoryService = new Mock<ICategoryService>();
			var mockLogger = new Mock<ILogger<CategoryController>>();

			var controller = new CategoryController(
				mockCategoryService.Object,
				mockLogger.Object);

			// Setup the behavior of GetCategoriesViewAsync() to return mock data
			var mockCategories = new List<CategoryViewModel>
			{
				new CategoryViewModel { Id = 1, Name = "Category 1", Image = "category1.jpg" },
				new CategoryViewModel { Id = 2, Name = "Category 2", Image = "category2.jpg" },
			};
			mockCategoryService.Setup(service => service.GetCategoriesViewAsync())
							   .ReturnsAsync(mockCategories);

			// Act
			var result = await controller.Index() as ViewResult;

			// Assert
			Assert.NotNull(result); // Check if the result is not null
			Assert.True(string.IsNullOrEmpty(result!.ViewName), "View name should not be null or empty.");
			// Verify that the correct data is passed to the view model
			var viewModel = result.Model as IEnumerable<CategoryViewModel>;
			Assert.NotNull(viewModel);
			Assert.AreEqual(mockCategories.Count, ((List<CategoryViewModel>)viewModel!).Count);
		}
		[Test]
		public async Task Assets_ShouldReturnCorrectViewResultWithAssets()
		{
			// Arrange
			var assetId1 = Guid.NewGuid();
			var assetId2 = Guid.NewGuid();
			var sortOrder = "title";
			int categoryId = 1;
			var page = 1;
			var itemsPerPage = 1;

			var mockCategoryService = new Mock<ICategoryService>();
			var mockLogger = new Mock<ILogger<CategoryController>>();

			var controller = new CategoryController(
				mockCategoryService.Object, 
				mockLogger.Object);

			// Setup the behavior of GetAssetsByCategoryIdAsync() to return mock data
			var mockAssets = new List<AssetViewModel>
			{
				new AssetViewModel { Id = assetId1, Title = "Asset 1", Thumbnail = "thumbnail1.jpg", Price = 100 },
				new AssetViewModel { Id = assetId2, Title = "Asset 2", Thumbnail = "thumbnail2.jpg", Price = 200 },
			};
			mockCategoryService.Setup(service => service.GetAssetsByCategoryIdAsync(categoryId))
							   .ReturnsAsync(mockAssets);

			// Act
			var result = await controller.Assets(sortOrder, categoryId, page, itemsPerPage) as ViewResult;

			// Assert
			Assert.NotNull(result); // Check if the result is not null
			Assert.AreEqual("~/Views/Asset/All.cshtml", result!.ViewName); // Check if the correct view is returned

			// Verify that the correct data is passed to the view model
			var viewModel = result.Model as PaginatedAssetViewModel;
			Assert.NotNull(viewModel);
			Assert.AreEqual(categoryId, viewModel!.CategoryId);

			// Verify that the correct data is paginated and sorted
			var expectedPaginatedData = mockAssets
			.Skip((page - 1) * itemsPerPage)
			.Take(itemsPerPage)
			.ToList(); // Convert to list before sorting

			switch (sortOrder)
			{
				case "price":
					expectedPaginatedData.Sort((asset1, asset2) => asset1.Price.CompareTo(asset2.Price));
					break;
				case "title":
					expectedPaginatedData.Sort((asset1, asset2) => string.Compare(asset1.Title, asset2.Title, StringComparison.Ordinal));
					break;
				case "uploadDate":
					expectedPaginatedData.Sort((asset1, asset2) => asset1.UploadDate.CompareTo(asset2.UploadDate));
					break;
					// Add more cases for other sorting orders if needed
			}

			CollectionAssert.AreEqual(expectedPaginatedData, viewModel.Items);
		}
	}


}
