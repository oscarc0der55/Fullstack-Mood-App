using MoodAppBE.Models;
using Microsoft.AspNetCore.Identity;
using System.Runtime.CompilerServices;

namespace MoodAppBE.Service
{
    public static class DataSeeding
    {
        public static async Task SeedAdminUser(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<int>>>();

            var admin = await userManager.FindByEmailAsync("admin@mood.se");

            if (admin != null)
            {
                return;
            }

            // Create the Admin role if it doesn't exist
            var adminRoleExists = await roleManager.RoleExistsAsync("Admin");
            if (!adminRoleExists)
            {
                await roleManager.CreateAsync(new IdentityRole<int> { Name = "Admin" });
            }

            admin = new User
            {
                UserName = "admin@mood.se",
                Email = "admin@mood.se",
                EmailConfirmed = true
            };
            var result = await userManager.CreateAsync(admin, "Admin123!");
            if (!result.Succeeded)
            {
                throw new InvalidOperationException($"Failed to create admin user: {string.Join(", ", result.Errors.Select(e => e.Description))}");
            }

            // FIX: Update the security stamp to ensure it's properly initialized
            await userManager.UpdateSecurityStampAsync(admin);

            await userManager.AddToRoleAsync(admin, "Admin");
        }
    }
}
