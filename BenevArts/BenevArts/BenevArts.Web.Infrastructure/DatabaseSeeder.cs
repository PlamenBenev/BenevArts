using BenevArts.Data.Models;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BenevArts.Web.Infrastructure
{
    public class DatabaseSeeder
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole<Guid>> roleManager;
        private readonly IConfiguration configuration;
        public DatabaseSeeder(UserManager<ApplicationUser> _userManager,
            RoleManager<IdentityRole<Guid>> _roleManager,
            IConfiguration _configuration)
        {
            userManager = _userManager;
            roleManager = _roleManager;
            configuration = _configuration;
        }

        public async Task SeedRolesAsync()
        {
            // Seed Roles
            var roles = new string[] { "Admin", "Seller", "User" };
            foreach (var roleName in roles)
            {
                var roleExists = await roleManager.RoleExistsAsync(roleName);
                if (!roleExists)
                {
                    var role = new IdentityRole<Guid>(roleName);
                    await roleManager.CreateAsync(role);
                }
            }

            var adminUsername = configuration["Admin:AdminUsername"];
            var adminEmail = configuration["Admin:AdminEmail"];
            var adminPassword = configuration["Admin:AdminPassword"];

            // Create the Admin user if it doesn't exist
            if (!await userManager.Users.AnyAsync(u => u.UserName == adminUsername))
            {
                var adminUser = new ApplicationUser
                {
                    UserName = adminUsername,
                    Email = adminEmail
                };

                // Create the Admin user with a password
                await userManager.CreateAsync(adminUser, adminPassword!);

                // Assign the "Admin" role to the user
                await userManager.AddToRoleAsync(adminUser, "Admin");
            }
        }
    }
}