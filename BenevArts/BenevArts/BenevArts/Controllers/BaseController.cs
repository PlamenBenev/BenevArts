using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BenevArts.Web.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
        protected string GetUserId()
        {
            string userId = string.Empty;

            if (userId != null)
            {
                userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            }

            return userId!;
        }
        protected string GetUsername()
        {
            string username = string.Empty;

            if (username != null)
            {
                username = User.FindFirstValue(ClaimTypes.Name);
            }

            return username!;
        }
        protected string GetEmail()
        {
            string email = string.Empty;

            if (email != null)
            {
				email = User.FindFirstValue(ClaimTypes.Email);
            }

            return email!;
        }
        protected async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }

            if (!await roleManager.RoleExistsAsync("Seller"))
            {
                await roleManager.CreateAsync(new IdentityRole("Seller"));
            }

            if (!await roleManager.RoleExistsAsync("ApplicationUser"))
            {
                await roleManager.CreateAsync(new IdentityRole("ApplicationUser"));
            }
        }
    }

}
