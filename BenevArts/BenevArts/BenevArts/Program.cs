using AutoMapper;
using BenevArts.Data;
using BenevArts.Data.Models;
using BenevArts.Services.Data;
using BenevArts.Services.Data.Interfaces;
using BenevArts.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<BenevArtsDbContext>(options =>
	options.UseSqlServer(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
	options.SignIn.RequireConfirmedAccount = false)
	.AddEntityFrameworkStores<BenevArtsDbContext>();

builder.Services.AddControllersWithViews();

builder.Services.Configure<IdentityOptions>(
	options =>
	{
		options.Password.RequireNonAlphanumeric = false;
		options.Password.RequireDigit = false;
		options.Password.RequireUppercase = false;
	});

builder.Services.AddScoped<IImageService, ImageService>();
builder.Services.AddScoped<IAssetService, AssetService>();
builder.Services.AddScoped<ICommentService, CommentService>();

builder.Services.Configure<IISServerOptions>(options =>
{
	options.MaxRequestBodySize = int.MaxValue;
});


var mapperConfig = new MapperConfiguration(mc =>
{
	mc.AddProfile(new MappingProfile());
});

IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

builder.Services.AddMvc();

builder.Services.AddSingleton(provider =>
{
	var configuration = provider.GetRequiredService<IConfiguration>();
	return configuration.GetValue<string>("UploadFolderPath");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseMigrationsEndPoint();
}
else
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapControllerRoute(
	name: "remove",
	pattern: "asset/remove/{id}",
	defaults: new { controller = "Asset", action = "Remove" });
app.MapControllerRoute(
	name: "comment",
	pattern: "Comment/PostComment/{id?}",
	defaults: new { controller = "Comment", action = "PostComment" });

app.MapRazorPages();

app.Run();
