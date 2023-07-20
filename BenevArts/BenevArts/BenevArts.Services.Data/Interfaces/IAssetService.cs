﻿using BenevArts.Web.ViewModels.Home;

namespace BenevArts.Services.Data.Interfaces
{
    public interface IAssetService
    {
        // Get
        Task<IEnumerable<AssetViewModel>> GetAllAssetsAsync();
        Task<IEnumerable<AssetViewModel>> GetFavoritesAsync(string userId);
        Task<IEnumerable<AssetViewModel>> GetSearchResultAsync(string query);
        Task<AssetViewModel> GetAssetByIdAsync(Guid id,string userId);

        // Post
        Task AddAssetAsync(AddAssetViewModel model, string userId);
        Task RemoveAssetAsync(Guid assetId, string userId);
        Task<bool> EditAssetAsync(EditAssetViewModel model);
        Task<EditAssetViewModel> GetEditByIdAsync(Guid id);
    }
}
