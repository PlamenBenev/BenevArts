using AutoMapper;
using BenevArts.Data;
using BenevArts.Data.Models;
using BenevArts.Services.Data.Interfaces;
using BenevArts.Web.ViewModels.Home;
using Microsoft.EntityFrameworkCore;

namespace BenevArts.Services.Data
{
    public class AssetService : IAssetService
    {
        private readonly BenevArtsDbContext context;
        private readonly IMapper mapper;
        public AssetService(BenevArtsDbContext _context, IMapper _mapper)
        {
            context = _context;
            mapper = _mapper;
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            return await context.Categories.ToListAsync();
        }

        public async Task AddAssetAsync(AddAssetViewModel model, string SellerId)
        {
            var fileName = Path.GetFileName(model.ZipFileName.FileName);
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", fileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await model.ZipFileName.CopyToAsync(fileStream);
            }

            var asset = mapper.Map<Asset>(model);
            asset.SellerId = Guid.Parse(SellerId);

            await context.Assets.AddAsync(asset);
            await context.SaveChangesAsync();
        }

        public async Task<AssetViewModel> GetAssetByIdAsync(Guid id)
        {
            Asset asset = await context.Assets.FirstOrDefaultAsync(a => a.Id == id);

            AssetViewModel viewModel = mapper.Map<AssetViewModel>(asset);

            return viewModel;

        }

    }
}
