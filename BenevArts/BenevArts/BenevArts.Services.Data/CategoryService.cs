﻿using BenevArts.Data;
using BenevArts.Data.Models;
using BenevArts.Services.Data.Interfaces;
using BenevArts.Web.ViewModels.Home;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace BenevArts.Services.Data
{
    public class CategoryService : ICategoryService
    {
        private readonly BenevArtsDbContext context;

        public CategoryService(BenevArtsDbContext _context)
        {
            context = _context;
        }

        // Get
        public async Task<IEnumerable<AssetViewModel>> GetAssetsByCategoryIdAsync(int categoryId)
        {
            return await context.Assets
                  .Where(a => a.CategoryId == categoryId)
                  .Select(a => new AssetViewModel
                  {
                      Id = a.Id,
                      Title = a.Title,
                      Thumbnail = a.Thumbnail,
                      Price = a.Price,
                      UploadDate = a.UploadDate,
                      Seller = a.Seller.SellerName
                  })
                  .ToListAsync();
        }

        public async Task<IEnumerable<CategoryViewModel>> GetCategoriesViewAsync()
        {
            return await context.Categories
                .Select(ct => new CategoryViewModel
                {
                    Id = ct.Id,
                    Name = ct.Name,
                    Image = ct.Image,
                })
                .ToListAsync();
        }
        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            return await context.Categories
                .Select(ct => new Category
                {
                    Id = ct.Id,
                    Name = ct.Name,
                    Image = ct.Image,
                })
                .ToListAsync();
        }
    }
}
