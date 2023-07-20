using BenevArts.Services.Data;
using BenevArts.Services.Data.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace BenevArts.Web.Infrastructure
{
    public static class BuilderExtensions 
    {
        public static void AddApplicationService(this IServiceCollection services)
        {
            services.AddScoped<IImageService, ImageService>();
            services.AddScoped<IAssetService, AssetService>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<ILikeService, LikeService>();
            services.AddScoped<IFavoriteService, FavoriteService>();
            services.AddScoped<ICategoryService, CategoryService>();
			services.AddScoped<ISellerService, SellerService>();
		}
		public static void Configure(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();
                SeedRolesAsync(roleManager).Wait();
            }
        }

        private static async Task SeedRolesAsync(RoleManager<IdentityRole<Guid>> roleManager)
        {
            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                await roleManager.CreateAsync(new IdentityRole<Guid>("Admin"));
            }

            if (!await roleManager.RoleExistsAsync("Seller"))
            {
                await roleManager.CreateAsync(new IdentityRole<Guid>("Seller"));
            }

            if (!await roleManager.RoleExistsAsync("User"))
            {
                await roleManager.CreateAsync(new IdentityRole<Guid>("User"));
            }
        }
    }
}