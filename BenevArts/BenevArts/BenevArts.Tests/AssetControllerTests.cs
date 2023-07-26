using AutoMapper;
using Moq;
using NUnit.Framework;

using BenevArts.Web.ViewModels.Home;
using BenevArts.Services.Data.Interfaces;
using BenevArts.Web.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
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

		var assetId1 = Guid.NewGuid();
		var assetId2 = Guid.NewGuid();

		// Mock the assetService
		var mockAssetService = new Mock<IAssetService>();
		var mockLikeService = new Mock<ILikeService>();
		var mockCategoryService = new Mock<ICategoryService>();
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
			mockCategoryService.Object);

		// Act
		var result = await controller.All() as ViewResult;

		// Assert
		Assert.NotNull(result); // Check if the result is not null
		Assert.True(string.IsNullOrEmpty(result!.ViewName), "View name should not be null or empty.");

		// Verify that the correct data is passed to the view model
		var viewModel = result.Model as PaginatedAssetViewModel;
		Assert.NotNull(viewModel);
		Assert.AreEqual(page, viewModel!.CurrentPage);
		Assert.AreEqual(itemsPerPage, viewModel.ItemsPerPage);
		Assert.AreEqual(mockData.Count, viewModel.TotalItems);

		// Verify that the correct data is paginated
		var expectedPaginatedData = mockData.Skip((page - 1) * itemsPerPage).Take(itemsPerPage);
		CollectionAssert.AreEqual(expectedPaginatedData, viewModel.Items);
	}

	[Test]
	public async Task Add_ShouldReturnCorrectViewResult()
	{
		// Arrange
		var mockCategoryService = new Mock<ICategoryService>();
		var mockMapper = new Mock<IMapper>();
		var mockAssetService = new Mock<IAssetService>();
		var mockLikeService = new Mock<ILikeService>();

		// Setup the behavior of GetCategoriesAsync() to return mock data
		var mockCategories = new List<CategoryViewModel>
		{
			new CategoryViewModel { Id = 1, Name = "Category 1" },
			new CategoryViewModel { Id = 2, Name = "Category 2" }
		};
		mockCategoryService.Setup(service => service.GetCategoriesViewAsync())
						   .ReturnsAsync(mockCategories);

		// Create the controller instance and pass the mock services
		var controller = new AssetController(
			mockAssetService.Object,
			mockLikeService.Object,
			mockCategoryService.Object);
		// Act
		var result = await controller.Add() as ViewResult;

		// Assert
		Assert.NotNull(result); // Check if the result is not null
		Assert.True(string.IsNullOrEmpty(result!.ViewName), "View name should not be null or empty.");

		// Verify that the correct data is passed to the view model
		var viewModel = result.Model as AddAssetViewModel;
		Assert.NotNull(viewModel);
		CollectionAssert.AreEqual(mockCategories, viewModel!.Categories);
		// You can check other properties of the view model as needed
	}

	[Test]
	public async Task Edit_ShouldReturnCorrectViewResult()
	{
		// Arrange
		var assetId = Guid.NewGuid();

		var mockCategoryService = new Mock<ICategoryService>();
		var mockMapper = new Mock<IMapper>();
		var mockAssetService = new Mock<IAssetService>();
		var mockLikeService = new Mock<ILikeService>();
		// Setup the behavior of GetEditByIdAsync() to return mock data
		var mockAssetData = new EditAssetViewModel
		{
			Id = assetId,
			Title = "Sample Asset",
			Description = "Sample Description",
			CGIModel = true,
			Textures = false,
			Materials = true,
			Animated = false,
			Rigged = true,
			LowPoly = false,
			PBR = true,
			UVUnwrapped = false,
			Price = 100.0m,
			CategoryId = 1,
			Categories = new List<CategoryViewModel>
			{
				new CategoryViewModel { Id = 1, Name = "Category 1" },
				new CategoryViewModel { Id = 2, Name = "Category 2" },
			},
			// Add other properties as needed for the EditAssetViewModel
		};
		mockAssetService.Setup(service => service.GetEditByIdAsync(assetId))
						.ReturnsAsync(mockAssetData);

		// Create the controller instance and pass the mock assetService
		var controller = new AssetController(
			mockAssetService.Object,
			mockLikeService.Object,
			mockCategoryService.Object);

		// Act
		var result = await controller.Edit(assetId) as ViewResult;

		// Assert
		Assert.NotNull(result); // Check if the result is not null
		Assert.True(string.IsNullOrEmpty(result!.ViewName), "View name should not be null or empty.");

		// Verify that the correct data is passed to the view model
		var viewModel = result.Model as EditAssetViewModel;
		Assert.NotNull(viewModel);
		Assert.AreEqual(mockAssetData.Id, viewModel!.Id);
		Assert.AreEqual(mockAssetData.Title, viewModel.Title);
		Assert.AreEqual(mockAssetData.Description, viewModel.Description);
		Assert.AreEqual(mockAssetData.CGIModel, viewModel.CGIModel);
		Assert.AreEqual(mockAssetData.Textures, viewModel.Textures);
		Assert.AreEqual(mockAssetData.Materials, viewModel.Materials);
		Assert.AreEqual(mockAssetData.Animated, viewModel.Animated);
		Assert.AreEqual(mockAssetData.Rigged, viewModel.Rigged);
		Assert.AreEqual(mockAssetData.LowPoly, viewModel.LowPoly);
		Assert.AreEqual(mockAssetData.PBR, viewModel.PBR);
		Assert.AreEqual(mockAssetData.UVUnwrapped, viewModel.UVUnwrapped);
		Assert.AreEqual(mockAssetData.Price, viewModel.Price);
		Assert.AreEqual(mockAssetData.CategoryId, viewModel.CategoryId);

		// Assert the categories list
		Assert.NotNull(viewModel.Categories);
		Assert.AreEqual(2, viewModel.Categories.Count());

		// Add other assertions for properties and lists as needed
	}

	[Test]
	public async Task Favorites_ShouldReturnCorrectViewResult()
	{
		// Arrange
		var page = 1;
		var itemsPerPage = 1;
		var userId = "sampleUserId"; // Replace with a sample user ID

		// Mock the assetService
		var mockCategoryService = new Mock<ICategoryService>();
		var mockMapper = new Mock<IMapper>();
		var mockAssetService = new Mock<IAssetService>();
		var mockLikeService = new Mock<ILikeService>();
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
		mockAssetService.Setup(service => service.GetFavoritesAsync(userId))
						.ReturnsAsync(mockFavoritesData);

		// Create the controller instance and pass the mock assetService
		var controller = new AssetController(
			mockAssetService.Object,
			mockLikeService.Object,
			mockCategoryService.Object);

		var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
		{
		new Claim(ClaimTypes.Name, "TestUser"), // Replace "TestUser" with the desired username
        new Claim(ClaimTypes.NameIdentifier, userId),
		new Claim(ClaimTypes.Role, "User") // Replace "User" with the user's role
		}));

		controller.ControllerContext = new ControllerContext
		{
			HttpContext = new DefaultHttpContext { User = user }
		};

		// Act
		var result = await controller.Favorites(page, itemsPerPage) as ViewResult;

		// Assert
		Assert.NotNull(result); // Check if the result is not null
		Assert.AreEqual("~/Views/Asset/All.cshtml", result!.ViewName); // Check if the correct view is returned

		// Verify that the correct data is passed to the view model
		var viewModel = result.Model as PaginatedAssetViewModel;
		Assert.NotNull(viewModel);
		Assert.AreEqual(page, viewModel!.CurrentPage);
		Assert.AreEqual(itemsPerPage, viewModel.ItemsPerPage);
		Assert.AreEqual(mockFavoritesData.Count, viewModel.TotalItems);

		// Assert the favorites list
		CollectionAssert.AreEqual(mockFavoritesData.Skip((page - 1) * itemsPerPage).Take(itemsPerPage).ToList(), viewModel.Items);

		// Add other assertions for properties and lists as needed
	}

	[Test]
	public async Task MyStore_ShouldReturnCorrectViewResult()
	{
		// Arrange
		var page = 1;
		var itemsPerPage = 1;
		var userId = "sampleUserId"; // Replace with a sample user ID

		// Mock the assetService
		var mockCategoryService = new Mock<ICategoryService>();
		var mockMapper = new Mock<IMapper>();
		var mockAssetService = new Mock<IAssetService>();
		var mockLikeService = new Mock<ILikeService>();
		// Setup the behavior of GetMyStoreAsync() to return mock data
		var mockMyStoreData = new List<AssetViewModel>
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
		mockAssetService.Setup(service => service.GetMyStoreAsync(userId))
						.ReturnsAsync(mockMyStoreData);

		// Create the controller instance and pass the mock assetService
		var controller = new AssetController(
			mockAssetService.Object,
			mockLikeService.Object,
			mockCategoryService.Object);

		var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
		{
			new Claim(ClaimTypes.Name, "TestUser"), // Replace "TestUser" with the desired username
            new Claim(ClaimTypes.NameIdentifier, userId),
			new Claim(ClaimTypes.Role, "Seller") // Replace "Seller" with the user's role
        }));

		controller.ControllerContext = new ControllerContext
		{
			HttpContext = new DefaultHttpContext { User = user }
		};

		// Act
		var result = await controller.MyStore(page, itemsPerPage) as ViewResult;

		// Assert
		Assert.NotNull(result); // Check if the result is not null
		Assert.AreEqual("~/Views/Asset/All.cshtml", result!.ViewName); // Check if the correct view is returned

		// Verify that the correct data is passed to the view model
		var viewModel = result.Model as PaginatedAssetViewModel;
		Assert.NotNull(viewModel);
		Assert.AreEqual(page, viewModel!.CurrentPage);
		Assert.AreEqual(itemsPerPage, viewModel.ItemsPerPage);
		Assert.AreEqual(mockMyStoreData.Count, viewModel.TotalItems);

		// Assert the MyStore list
		CollectionAssert.AreEqual(mockMyStoreData.Skip((page - 1) * itemsPerPage).Take(itemsPerPage).ToList(), viewModel.Items);

		// Add other assertions for properties and lists as needed
	}

	[Test]
	public async Task Search_ShouldReturnCorrectViewResult()
	{
		// Arrange
		var page = 1;
		var itemsPerPage = 1;
		var query = "sampleQuery"; // Replace with a sample search query

		// Mock the assetService
		var mockCategoryService = new Mock<ICategoryService>();
		var mockMapper = new Mock<IMapper>();
		var mockAssetService = new Mock<IAssetService>();
		var mockLikeService = new Mock<ILikeService>();
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
			mockCategoryService.Object);

		// Act
		var result = await controller.Search(query, page, itemsPerPage) as ViewResult;

		// Assert
		Assert.NotNull(result); // Check if the result is not null
		Assert.True(string.IsNullOrEmpty(result!.ViewName), "View name should not be null or empty.");

		// Verify that the correct data is passed to the view model
		var viewModel = result.Model as PaginatedAssetViewModel;
		Assert.NotNull(viewModel);
		Assert.AreEqual(page, viewModel!.CurrentPage);
		Assert.AreEqual(itemsPerPage, viewModel.ItemsPerPage);

		// Assert the search result list
		CollectionAssert.AreEqual(mockSearchResult.Skip((page - 1) * itemsPerPage).Take(itemsPerPage).ToList(), viewModel.Items);
		Assert.AreEqual(mockSearchResult.Count, viewModel.TotalItems);

		// Add other assertions for properties and lists as needed
	}

	[Test]
	public async Task Details_ShouldReturnCorrectViewResult()
	{
		// Arrange
		var assetId = Guid.NewGuid(); // Replace with a sample asset ID
		var userId = "sampleUserId"; // Replace with a sample user ID

		// Mock the assetService
		var mockCategoryService = new Mock<ICategoryService>();
		var mockMapper = new Mock<IMapper>();
		var mockAssetService = new Mock<IAssetService>();
		var mockLikeService = new Mock<ILikeService>();
		// Setup the behavior of GetAssetByIdAsync() to return mock data
		var mockAssetViewModel = new AssetViewModel
		{
			Id = assetId,
			Title = "Sample Asset",
			Thumbnail = "thumbnail.jpg",
			Price = 100,
			UploadDate = DateTime.UtcNow,
			Seller = "Seller 1",
			// Add other properties as needed
		};
		mockAssetService.Setup(service => service.GetAssetByIdAsync(assetId, userId))
						.ReturnsAsync(mockAssetViewModel);

		// Create the controller instance and pass the mock assetService
		var controller = new AssetController(
			mockAssetService.Object,
			mockLikeService.Object,
			mockCategoryService.Object);

		var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
		{
			new Claim(ClaimTypes.Name, "TestUser"), // Replace "TestUser" with the desired username
			new Claim(ClaimTypes.NameIdentifier, userId),
			new Claim(ClaimTypes.Role, "User") // Replace "User" with the user's role
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
		// Add other assertions for properties as needed
	}

	[Test]
	public async Task Download_ShouldReturnCorrectFileResult()
	{
		// Arrange
		var assetId = Guid.NewGuid(); // Replace with a sample asset ID
		var userId = "sampleUserId"; // Replace with a sample user ID

		// Mock the assetService
		var mockCategoryService = new Mock<ICategoryService>();
		var mockMapper = new Mock<IMapper>();
		var mockAssetService = new Mock<IAssetService>();
		var mockLikeService = new Mock<ILikeService>();

		// Setup the behavior of GetAssetByIdAsync() to return null for a specific assetId
		mockAssetService.Setup(service => service.GetAssetByIdAsync(assetId, userId))
							.Returns(Task.FromResult<AssetViewModel?>(null)!);

		var controller = new AssetController(
			mockAssetService.Object,
			mockLikeService.Object,
			mockCategoryService.Object);

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
		Assert.AreEqual("~/Areas/Identity/Pages/Account/Login.cshtml", result.ViewName); // Check if the view name is correct
																						 // Add other assertions as needed
	}

}
