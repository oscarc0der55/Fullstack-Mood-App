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
            var admin = await userManager.FindByEmailAsync("admin@mood.se");

            if(admin != null)
            {
                return;
            }
            admin = new User
            {
                UserName = "admin@mood.se",
                Email = "admin@mood.se",
                EmailConfirmed = true
            };

            await userManager.CreateAsync(admin, "Admin123");
            await userManager.AddToRoleAsync(admin, "Admin");
        }
    }
}
