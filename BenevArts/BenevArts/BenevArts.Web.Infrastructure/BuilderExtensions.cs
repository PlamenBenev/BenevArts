using BenevArts.Data;
using BenevArts.Data.Models;
using BenevArts.Services.Data;
using BenevArts.Services.Data.Interfaces;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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
			services.AddScoped<IStoreService, StoreService>();

			services.AddScoped<DatabaseSeeder>();
		}
		public static void Configure(IApplicationBuilder app)
		{
            using var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();
            var configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();
            var context = scope.ServiceProvider.GetRequiredService<BenevArtsDbContext>();
            context.Database.Migrate();

            DatabaseSeeder databaseSeeder = new(userManager, roleManager, configuration);
            databaseSeeder.SeedRolesAsync().Wait();
        }

	}
}