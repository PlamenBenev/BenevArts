using AutoMapper;
using Moq;
using NUnit.Framework;

using BenevArts.Web.ViewModels.Home;
using BenevArts.Services.Data.Interfaces;
using BenevArts.Web.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

using System.Security.Claims;

[TestFixture]
public class AssetControllerTests
{

	[Test]
	public async Task All_ShouldReturnCorrectViewResult()
	{
		// Arrange
		var page = 1;
		var itemsPerPage = 1;
		var sortOrder = "title";

		var assetId1 = Guid.NewGuid();
		var assetId2 = Guid.NewGuid();

		// Mock the assetService
		var mockAssetService = new Mock<IAssetService>();
		var mockLikeService = new Mock<ILikeService>();
		var mockCategoryService = new Mock<ICategoryService>();
		var mockLogger = new Mock<ILogger<AssetController>>();
		// Setup the behavior of GetAllAssetsAsync() to return mock data
		var mockData = new List<AssetViewModel>
	{
		new AssetViewModel
		{
			Id = Guid.NewGuid(),
			Title = "Asset 1",
			Thumbnail = "thumbnail1.jpg",
			Price = 100,
			UploadDate = DateTime.UtcNow,
		},
		new AssetViewModel
		{
			Id = Guid.NewGuid(),
			Title = "Asset 2",
			Thumbnail = "thumbnail2.jpg",
			Price = 200,
			UploadDate = DateTime.UtcNow,
		},
	};
		mockAssetService.Setup(service => service.GetAllAssetsAsync())
						.ReturnsAsync(mockData);

		// Create the controller instance and pass the mock assetService
		var controller = new AssetController(
			mockAssetService.Object,
			mockLikeService.Object,
			mockLogger.Object);

		// Act
		var result = await controller.All(sortOrder, page, itemsPerPage) as ViewResult;

		// Assert
		Assert.NotNull(result); // Check if the result is not null
		Assert.True(string.IsNullOrEmpty(result!.ViewName), "View name should not be null or empty.");

		// Verify that the correct data is passed to the view model
		var viewModel = result.Model as PaginatedAssetViewModel;
		Assert.NotNull(viewModel);
		Assert.AreEqual(page, viewModel!.CurrentPage);
		Assert.AreEqual(itemsPerPage, viewModel.ItemsPerPage);
		Assert.AreEqual(mockData.Count, viewModel.TotalItems);

		// Verify that the correct data is paginated and sorted
		var expectedPaginatedData = mockData
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

	[Test]
	public async Task Search_ShouldReturnCorrectViewResult()
	{
		// Arrange
		var page = 1;
		var itemsPerPage = 1;
		var query = "sampleQuery";
		var sortOrder = "price"; // Set the desired sorting order

		// Mock the assetService
		var mockAssetService = new Mock<IAssetService>();
		var mockLikeService = new Mock<ILikeService>();
		var mockLogger = new Mock<ILogger<AssetController>>();

		// Setup the behavior of GetSearchResultAsync() to return mock data
		var mockSearchResult = new List<AssetViewModel>
	{
		new AssetViewModel
		{
			Title = "Asset 1",
			Thumbnail = "thumbnail1.jpg",
			Price = 100,
			UploadDate = DateTime.UtcNow,
			Seller = "Seller 1"
		},
		new AssetViewModel
		{
			Title = "Asset 2",
			Thumbnail = "thumbnail2.jpg",
			Price = 200,
			UploadDate = DateTime.UtcNow,
			Seller = "Seller 2"
		},
	};
		mockAssetService.Setup(service => service.GetSearchResultAsync(query))
						.ReturnsAsync(mockSearchResult);

		// Create the controller instance and pass the mock assetService
		var controller = new AssetController(
			mockAssetService.Object,
			mockLikeService.Object, 
			mockLogger.Object);

		// Act
		var result = await controller.Search(sortOrder, query, page, itemsPerPage) as ViewResult;

		// Assert
		Assert.NotNull(result); // Check if the result is not null
		Assert.AreEqual("~/Views/Asset/All.cshtml", result!.ViewName); // Check if the correct view is returned

		// Verify that the correct data is passed to the view model
		var viewModel = result.Model as PaginatedAssetViewModel;
		Assert.NotNull(viewModel);
		Assert.AreEqual(page, viewModel!.CurrentPage);
		Assert.AreEqual(itemsPerPage, viewModel.ItemsPerPage);
		Assert.AreEqual(sortOrder, result.ViewData["CurrentSortOrder"]); // Verify the sorting order is set

		// Assert the search result list
		CollectionAssert.AreEqual(mockSearchResult.Skip((page - 1) * itemsPerPage).Take(itemsPerPage).ToList(), viewModel.Items);
		Assert.AreEqual(mockSearchResult.Count, viewModel.TotalItems);
	}

	[Test]
	public async Task Details_ShouldReturnCorrectViewResult()
	{
		// Arrange
		var assetId = Guid.NewGuid();
		var userId = "sampleUserId";

		// Mock the assetService
		var mockMapper = new Mock<IMapper>();
		var mockAssetService = new Mock<IAssetService>();
		var mockLikeService = new Mock<ILikeService>();
		var mockLogger = new Mock<ILogger<AssetController>>();

		// Setup the behavior of GetAssetByIdAsync() to return mock data
		var mockAssetViewModel = new AssetViewModel
		{
			Id = assetId,
			Title = "Sample Asset",
			Thumbnail = "thumbnail.jpg",
			Price = 100,
			UploadDate = DateTime.UtcNow,
			Seller = "Seller 1",
		};
		mockAssetService.Setup(service => service.GetAssetByIdAsync(assetId, userId))
						.ReturnsAsync(mockAssetViewModel);

		// Create the controller instance and pass the mock assetService
		var controller = new AssetController(
			mockAssetService.Object,
			mockLikeService.Object, 
			mockLogger.Object);

		var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
		{
			new Claim(ClaimTypes.Name, "TestUser"),
			new Claim(ClaimTypes.NameIdentifier, userId),
			new Claim(ClaimTypes.Role, "User")
		}));

		controller.ControllerContext = new ControllerContext
		{
			HttpContext = new DefaultHttpContext { User = user }
		};
		// Act
		var result = await controller.Details(assetId) as ViewResult;

		// Assert
		Assert.NotNull(result); // Check if the result is not null
		Assert.True(string.IsNullOrEmpty(result!.ViewName), "View name should not be null or empty.");

		// Verify that the correct data is passed to the view model
		var viewModel = result.Model as AssetViewModel;
		Assert.NotNull(viewModel);
		Assert.AreEqual(mockAssetViewModel.Id, viewModel!.Id);
		Assert.AreEqual(mockAssetViewModel.Title, viewModel.Title);
		Assert.AreEqual(mockAssetViewModel.Thumbnail, viewModel.Thumbnail);
		Assert.AreEqual(mockAssetViewModel.Price, viewModel.Price);
		Assert.AreEqual(mockAssetViewModel.UploadDate, viewModel.UploadDate);
		Assert.AreEqual(mockAssetViewModel.Seller, viewModel.Seller);
	}

	[Test]
	public async Task Download_ShouldReturnCorrectFileResult()
	{
		// Arrange
		var assetId = Guid.NewGuid();
		var userId = "sampleUserId";

		// Mock the assetService
		var mockMapper = new Mock<IMapper>();
		var mockAssetService = new Mock<IAssetService>();
		var mockLikeService = new Mock<ILikeService>();
		var mockLogger = new Mock<ILogger<AssetController>>();

		// Setup the behavior of GetAssetByIdAsync() to return null for a specific assetId
		mockAssetService.Setup(service => service.GetAssetByIdAsync(assetId, userId))
							.Returns(Task.FromResult<AssetViewModel?>(null)!);

		var controller = new AssetController(
			mockAssetService.Object,
			mockLikeService.Object, 
			mockLogger.Object);

		// Create a mock ClaimsPrincipal to represent an authenticated user
		var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
		{
		new Claim(ClaimTypes.NameIdentifier, userId),
		new Claim(ClaimTypes.Role, "User")
		}));

		controller.ControllerContext = new ControllerContext
		{
			HttpContext = new DefaultHttpContext { User = user }
		};

		// Act
		var result = await controller.Download(assetId) as ViewResult;

		// Assert
		Assert.NotNull(result); // Check if the result is not null
		Assert.AreEqual("~/Areas/Identity/Pages/Account/Login.cshtml", result!.ViewName); // Check if the view name is correct
	}

	// Favorites
	[Test]
	public async Task Favorites_ShouldReturnCorrectViewResult()
	{
		// Arrange
		var page = 1;
		var itemsPerPage = 1;
		var userId = "sampleUserId";
		var sortOrder = "title";

		// Mock the favoriteService
		var mockFavoritesService = new Mock<IFavoriteService>();
		var mockLogger = new Mock<ILogger<FavoriteController>>();

		// Setup the behavior of GetFavoritesAsync() to return mock data
		var mockFavoritesData = new List<AssetViewModel>
		{
			new AssetViewModel
			{
				Id = Guid.NewGuid(),
				Title = "Asset 1",
				Thumbnail = "thumbnail1.jpg",
				Price = 100,
				UploadDate = DateTime.UtcNow,
				Seller = "Seller 1"
			},
			new AssetViewModel
			{
				Id = Guid.NewGuid(),
				Title = "Asset 2",
				Thumbnail = "thumbnail2.jpg",
				Price = 200,
				UploadDate = DateTime.UtcNow,
				Seller = "Seller 2"
			},
		};
		mockFavoritesService.Setup(service => service.GetFavoritesAsync(userId))
						.ReturnsAsync(mockFavoritesData);

		// Create the controller instance and pass the mock assetService
		var controller = new FavoriteController(
			mockFavoritesService.Object,
			mockLogger.Object);

		var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
		{
		new Claim(ClaimTypes.Name, "TestUser"),
		new Claim(ClaimTypes.NameIdentifier, userId),
		new Claim(ClaimTypes.Role, "User")
		}));

		controller.ControllerContext = new ControllerContext
		{
			HttpContext = new DefaultHttpContext { User = user }
		};

		// Act
		var result = await controller.Favorites(sortOrder, page, itemsPerPage) as ViewResult;

		// Assert
		Assert.NotNull(result); // Check if the result is not null
		Assert.AreEqual("~/Views/Asset/All.cshtml", result!.ViewName); // Check if the correct view is returned

		// Verify that the correct data is passed to the view model
		var viewModel = result.Model as PaginatedAssetViewModel;
		Assert.NotNull(viewModel);
		Assert.AreEqual(page, viewModel!.CurrentPage);
		Assert.AreEqual(itemsPerPage, viewModel.ItemsPerPage);
		Assert.AreEqual(mockFavoritesData.Count, viewModel.TotalItems);

		// Verify that the correct data is paginated and sorted
		var expectedPaginatedData = mockFavoritesData
		.Skip((page - 1) * itemsPerPage)
		.Take(itemsPerPage)
		.ToList(); // Convert to list before sorting
				   // Assert the favorites list
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
