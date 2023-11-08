using AutoMapper;

using BenevArts.Data;
using BenevArts.Data.Models;
using BenevArts.Web;
using BenevArts.Web.Infrastructure;
using BenevArts.Web.Infrastructure.ModelBinders;

using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Configuration.AddEnvironmentVariables(prefix: "POSTGRES_");

//var connectionString = builder.Configuration["DB"] ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<BenevArtsDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentity<ApplicationUser, IdentityRole<Guid>>()
    .AddEntityFrameworkStores<BenevArtsDbContext>()
.AddDefaultTokenProviders();

builder.Services.AddControllersWithViews();

builder.Services.Configure<IdentityOptions>(
    options =>
    {
        options.Password.RequireDigit = false;
        options.Password.RequireLowercase = false;
        options.Password.RequireUppercase = false;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequiredLength = 2;
    });

builder.Services.AddApplicationService();

// Set the maximum upload size to 50mb and the maximum size of single entry to 10mb.
builder.Services.Configure<IISServerOptions>(options =>
{
    options.MaxRequestBodySize = 50 * 1024 * 1024;
});

builder.Services.Configure<FormOptions>(options =>
{
    options.MemoryBufferThreshold = 50 * 1024 * 1024;
    options.ValueCountLimit = 10 * 1024 * 1024;
});

builder.WebHost.ConfigureKestrel(options =>
{
    options.Limits.MaxRequestBodySize = 50 * 1024 * 1024;
});

// Authentication Configuration
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.LogoutPath = "/Account/Logout";
        options.ExpireTimeSpan = TimeSpan.FromDays(7);
    });

// AutoMapper configuration
var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new MappingProfile());
});

IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

// Uploading configuration
builder.Services.AddSingleton(provider =>
{
    var configuration = provider.GetRequiredService<IConfiguration>();
    return configuration.GetValue<string>("UploadFolderPath") ?? "defaultPath";
});

// Decimal Model Binder Configuration
var cultureInfo = new CultureInfo("en-US"); // Sets the culture to use "." as the decimal separator
CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

builder.Services.AddMvc(options =>
{
    options.ModelBinderProviders.Insert(0, new DecimalModelBinderProvider());
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();

    app.UseDeveloperExceptionPage();
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
BuilderExtensions.Configure(app);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Category}/{action=Index}/{id?}");
app.MapControllerRoute(
    name: "remove",
    pattern: "asset/remove/{id}",
    defaults: new { controller = "Asset", action = "Remove" });
app.MapControllerRoute(
    name: "comment",
    pattern: "Comment/PostComment/{id?}",
    defaults: new { controller = "Comment", action = "PostComment" });

app.MapRazorPages();

await app.RunAsync();

