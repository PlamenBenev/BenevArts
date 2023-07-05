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
        public AssetService(BenevArtsDbContext _context)
        {
            context = _context;
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            return await context.Categories.ToListAsync();
        }

        public async Task AddAssetAsync(AddAssetViewModel model)
        {
            var fileName = Path.GetFileName(model.ZipFileName.FileName);
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", fileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await model.ZipFileName.CopyToAsync(fileStream);
            }
            // Save the file name in the Asset model
            var asset = new Asset
            {
                Title = model.Title,
                ZipFileName = fileName,

            };

            // Save the asset in the database
            await context.Assets.AddAsync(asset);
            await context.SaveChangesAsync();
        }

        public async Task<AssetViewModel> GetAssetByIdAsync(string id)
        {
            return await context.Assets
                    .Where(x => x.Id.ToString() == id)
                    .Select(e => new AssetViewModel()
                    {

                    })
                    .FirstOrDefaultAsync();

        }

    }
}
