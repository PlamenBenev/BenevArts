using AutoMapper;
using Moq;
using NUnit.Framework;

using BenevArts.Web.ViewModels.Home;
using BenevArts.Services.Data.Interfaces;
using BenevArts.Web.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

using System.Security.Claims;
using Castle.Core.Logging;
using Microsoft.Extensions.Logging;

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
	public async Task Add_ShouldReturnCorrectViewResult()
	{
		// Arrange
		var mockMapper = new Mock<IMapper>();
		var mockStoreService = new Mock<IStoreService>();
		var mockCategoryService = new Mock<ICategoryService>();
		var mockLogger = new Mock<ILogger<StoreController>>();

		// Setup the behavior of GetCategoriesAsync() to return mock data
		var mockCategories = new List<CategoryViewModel>
		{
			new CategoryViewModel { Id = 1, Name = "Category 1" },
			new CategoryViewModel { Id = 2, Name = "Category 2" }
		};
		mockCategoryService.Setup(service => service.GetCategoriesViewAsync())
						   .ReturnsAsync(mockCategories);

		// Create the controller instance and pass the mock services
		var controller = new StoreController(
			mockStoreService.Object,
			mockCategoryService.Object,
			mockLogger.Object);
		// Act
		var result = await controller.Add() as ViewResult;

		// Assert
		Assert.NotNull(result); // Check if the result is not null
		Assert.True(string.IsNullOrEmpty(result!.ViewName), "View name should not be null or empty.");

		// Verify that the correct data is passed to the view model
		var viewModel = result.Model as AddAssetViewModel;
		Assert.NotNull(viewModel);
		CollectionAssert.AreEqual(mockCategories, viewModel!.Categories);
	}

	[Test]
	public async Task Edit_ShouldReturnCorrectViewResult()
	{
		// Arrange
		var assetId = Guid.NewGuid();

		var mockCategoryService = new Mock<ICategoryService>();
		var mockStoreService = new Mock<IStoreService>();
		var mockLogger = new Mock<ILogger<StoreController>>();

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
		};
		mockStoreService.Setup(service => service.GetEditByIdAsync(assetId))
						.ReturnsAsync(mockAssetData);

		// Create the controller instance and pass the mock assetService
		var controller = new StoreController(
			mockStoreService.Object,
			mockCategoryService.Object,
			mockLogger.Object);

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
	}

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

	[Test]
	public async Task MyStore_ShouldReturnCorrectViewResult()
	{
		// Arrange
		var page = 1;
		var itemsPerPage = 1;
		var userId = "sampleUserId";
		var sortOrder = "title";

		// Mock the storeService
		var mockCategoryService = new Mock<ICategoryService>();
		var mockStoreService = new Mock<IStoreService>();
		var mockLogger = new Mock<ILogger<StoreController>>();

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
		mockStoreService.Setup(service => service.GetMyStoreAsync(userId))
						.ReturnsAsync(mockMyStoreData);

		// Create the controller instance and pass the mock storeService
		var controller = new StoreController(
			mockStoreService.Object,
			mockCategoryService.Object,
			mockLogger.Object);

		var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
		{
			new Claim(ClaimTypes.Name, "TestUser"),
			new Claim(ClaimTypes.NameIdentifier, userId),
			new Claim(ClaimTypes.Role, "Seller")
		}));

		controller.ControllerContext = new ControllerContext
		{
			HttpContext = new DefaultHttpContext { User = user }
		};

		// Act
		var result = await controller.MyStore(sortOrder, page, itemsPerPage) as ViewResult;

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
		var expectedPaginatedData = mockMyStoreData
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

	[Test]
	public async Task Add_ShouldRedirectToMyStoreWhenModelIsValid()
	{
		// Arrange
		var userId = "sampleUserId";

		// Mock the assetService
		var mockCategoryService = new Mock<ICategoryService>();
		var mockStoreService = new Mock<IStoreService>();
		var mockLogger = new Mock<ILogger<StoreController>>();

		// Create the controller instance and pass the mock assetService
		var controller = new StoreController(
			mockStoreService.Object,
			mockCategoryService.Object, 
			mockLogger.Object);

		var model = new AddAssetViewModel
		{
			Title = "Sample Asset",
			ZipFileName = new FormFile(new MemoryStream(new byte[0]), 0, 0, "zipfile", "sample.zip"),
			Thumbnail = new FormFile(new MemoryStream(new byte[0]), 0, 0, "thumbnail", "sample.jpg"),
			Description = "This is a sample asset",
		};

		// Mock the HttpContext and User to represent an authenticated user
		var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
		{
		new Claim(ClaimTypes.NameIdentifier, userId),
		new Claim(ClaimTypes.Role, "Seller") // Assuming the user has the "Seller" role
		}));

		controller.ControllerContext = new ControllerContext
		{
			HttpContext = new DefaultHttpContext { User = user }
		};

		// Set ModelState to be invalid to simulate an invalid model
		controller.ModelState.AddModelError("Title", "Title is required");

		// Act
		var result = await controller.Add(model);

		// Assert
		Assert.NotNull(result); // Check if the result is not null
		Assert.IsInstanceOf<ViewResult>(result); // Check if the result is a ViewResult
	}

	[Test]
	public async Task Add_ShouldReturnViewWithModelWhenModelIsInvalid()
	{
		// Arrange
		var userId = "sampleUserId";

		// Mock the assetService
		var mockCategoryService = new Mock<ICategoryService>();
		var mockStoreService = new Mock<IStoreService>();
		var mockLogger = new Mock<ILogger<StoreController>>();

		// Create the controller instance and pass the mock assetService
		var controller = new StoreController(
			mockStoreService.Object,
			mockCategoryService.Object,
			mockLogger.Object);

		// Simulate model state errors to make the model invalid
		controller.ModelState.AddModelError("PropertyName", "Error Message");

		var model = new AddAssetViewModel
		{
			Title = "Sample Asset",
			ZipFileName = new FormFile(new MemoryStream(new byte[0]), 0, 0, "zipfile", "sample.zip"),
			Thumbnail = new FormFile(new MemoryStream(new byte[0]), 0, 0, "thumbnail", "sample.jpg"),
			Description = "This is a sample asset",
		};

		// Mock the HttpContext and User to represent an authenticated user
		var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
		{
		new Claim(ClaimTypes.NameIdentifier, userId),
		new Claim(ClaimTypes.Role, "Seller") // Assuming the user has the "Seller" role
		}));

		controller.ControllerContext = new ControllerContext
		{
			HttpContext = new DefaultHttpContext { User = user }
		};

		// Act
		var result = await controller.Add(model) as ViewResult;

		// Assert
		Assert.NotNull(result); // Check if the result is not null
		Assert.AreEqual(model, result!.Model); // Check if the model is passed back to the view
	}

	[Test]
	public async Task Edit_ShouldRedirectToDetailsWhenModelIsValid()
	{
		// Arrange
		var userId = "sampleUserId";
		var assetId = Guid.NewGuid();

		// Mock the assetService
		var mockCategoryService = new Mock<ICategoryService>();
		var mockStoreService = new Mock<IStoreService>();
		var mockLogger = new Mock<ILogger<StoreController>>();

		// Create the controller instance and pass the mock assetService
		var controller = new StoreController(
			mockStoreService.Object,
			mockCategoryService.Object,
			mockLogger.Object);

		var model = new EditAssetViewModel
		{
			Id = assetId,
			Title = "Sample Asset",
			ZipFile = new FormFile(new MemoryStream(new byte[0]), 0, 0, "zipfile", "sample.zip"),
			ThumbnailFile = new FormFile(new MemoryStream(new byte[0]), 0, 0, "thumbnail", "sample.jpg"),
			Description = "This is a sample asset",
		};

		// Mock the HttpContext and User to represent an authenticated user
		var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
		{
		new Claim(ClaimTypes.NameIdentifier, userId),
		new Claim(ClaimTypes.Role, "Seller") // Assuming the user has the "Seller" role
		}));

		controller.ControllerContext = new ControllerContext
		{
			HttpContext = new DefaultHttpContext { User = user }
		};

		// Set ModelState to be valid to simulate a valid model
		controller.ModelState.Clear();

		// Act
		var result = await controller.Edit(model, assetId);

		// Assert
		Assert.NotNull(result); // Check if the result is not null
		Assert.IsInstanceOf<RedirectToActionResult>(result); // Check if the result is a RedirectToActionResult
	}

	[Test]
	public async Task Edit_ShouldReturnViewResultWhenModelIsInvalid()
	{
		// Arrange
		var userId = "sampleUserId";
		var assetId = Guid.NewGuid();

		// Mock the assetService
		var mockCategoryService = new Mock<ICategoryService>();
		var mockStoreService = new Mock<IStoreService>();
		var mockLogger = new Mock<ILogger<StoreController>>();

		// Create the controller instance and pass the mock assetService
		var controller = new StoreController(
			mockStoreService.Object,
			mockCategoryService.Object,
			mockLogger.Object);

		var model = new EditAssetViewModel
		{
			Id = assetId,
			Title = "Sample Asset",

		};

		// Mock the HttpContext and User to represent an authenticated user
		var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
		{
		new Claim(ClaimTypes.NameIdentifier, userId),
		new Claim(ClaimTypes.Role, "Seller") // Assuming the user has the "Seller" role
		}));

		controller.ControllerContext = new ControllerContext
		{
			HttpContext = new DefaultHttpContext { User = user }
		};

		// Set ModelState to be invalid to simulate an invalid model
		controller.ModelState.AddModelError("Title", "Title is required");

		// Act
		var result = await controller.Edit(model, assetId);

		// Assert
		Assert.NotNull(result); // Check if the result is not null
		Assert.IsInstanceOf<ViewResult>(result); // Check if the result is a ViewResult
	}

	[Test]
	public async Task Remove_ShouldRedirectToAllActionAfterSuccessfulRemoval()
	{
		// Arrange
		var userId = "sampleUserId";
		var assetId = Guid.NewGuid();

		// Mock the assetService
		var mockCategoryService = new Mock<ICategoryService>();
		var mockStoreService = new Mock<IStoreService>();
		var mockLogger = new Mock<ILogger<StoreController>>();

		// Create the controller instance and pass the mock assetService
		var controller = new StoreController(
			mockStoreService.Object,
			mockCategoryService.Object,
			mockLogger.Object);

		// Mock the HttpContext and User to represent an authenticated user
		var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
		{
		new Claim(ClaimTypes.NameIdentifier, userId),
		new Claim(ClaimTypes.Role, "Seller") // Assuming the user has the "Seller" role
		}));

		controller.ControllerContext = new ControllerContext
		{
			HttpContext = new DefaultHttpContext { User = user }
		};

		// Act
		var result = await controller.Remove(assetId);

		// Assert
		Assert.NotNull(result); // Check if the result is not null
		Assert.IsInstanceOf<RedirectToActionResult>(result); // Check if the result is a RedirectToActionResult

		// Assert the action name and controller name in the RedirectToActionResult
		var redirectResult = (RedirectToActionResult)result;
		Assert.AreEqual("All", redirectResult.ActionName); // Ensure it redirects to the "All" action
		Assert.AreEqual("Asset",redirectResult.ControllerName); // Ensure it stays in the current controller

		mockStoreService.Verify(service => service.RemoveAssetAsync(assetId, userId), Times.Once);
	}
}
