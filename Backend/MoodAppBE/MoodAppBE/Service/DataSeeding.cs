using MoodAppBE.Models;
using Microsoft.AspNetCore.Identity;
using System.Runtime.CompilerServices;
using MoodAppBE.Data;
using Microsoft.EntityFrameworkCore;

namespace MoodAppBE.Service
{
    public static class DataSeeding
    {

        public static async Task SeedData(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<int>>>();
                var context = scope.ServiceProvider.GetRequiredService<MoodAppDBContext>();

                await SeedAdminUser(userManager, roleManager, context);
                await SeedUsers(userManager, roleManager, context);
            }
        }
        private static async Task SeedAdminUser(UserManager<User> userManager, RoleManager<IdentityRole<int>> roleManager, MoodAppDBContext context)
        {
            var admin = await userManager.FindByEmailAsync("admin@mood.se");

            if (admin == null)
            {
                var newAdmin = new User
                {
                    UserName = "admin@mood.se",
                    Email = "admin@mood.se",
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(newAdmin, "Admin123!");
                if (!result.Succeeded)
                {
                    throw new InvalidOperationException($"Failed to create admin user: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                }
                await userManager.UpdateSecurityStampAsync(newAdmin);

                // Create the Admin role if it doesn't exist
                var adminRoleExists = await roleManager.RoleExistsAsync("Admin");
                if (!adminRoleExists)
                {
                    await roleManager.CreateAsync(new IdentityRole<int> { Name = "Admin" });
                }

                // Now assign the user to the role
                await userManager.AddToRoleAsync(newAdmin, "Admin");

                context.ChangeTracker.Clear();
            }
        }

        private static async Task SeedUsers(UserManager<User> userManager, RoleManager<IdentityRole<int>> roleManager, MoodAppDBContext context)
        {
            var user1 = await context.Users.FirstOrDefaultAsync(u => u.SeedPos == 2);
            var user2 = await context.Users.FirstOrDefaultAsync(u => u.SeedPos == 3);

            const string seedEmail1 = "user1@gmail.com";
            const string seedEmail2 = "test2@gmail.com";

            if (user1 == null)
            {
                user1 = new User { UserName = seedEmail1, Email = seedEmail1, SeedPos = 2 };
                var result = await userManager.CreateAsync(user1, "Password123!");

                if (result == null)
                {
                    throw new InvalidOperationException("Failed to create user");
                }

                var userRoleExists = await roleManager.RoleExistsAsync("User");
                if (!userRoleExists)
                {
                    await roleManager.CreateAsync(new IdentityRole<int> { Name = "User" });
                }
                await userManager.AddToRoleAsync(user1, "User");
            }
            else
            {
                user1.Email = seedEmail1;
                user1.NormalizedEmail = seedEmail1.Normalize();

                user1.UserName = seedEmail1;
                user1.NormalizedUserName = seedEmail1.Normalize();

                await userManager.UpdateAsync(user1);
            }

            if (user2 == null)
            {
                user2 = new User { UserName = seedEmail2, Email = seedEmail2, SeedPos = 3 };
                var result = await userManager.CreateAsync(user2, "Password123!");

                if (result == null)
                {
                    throw new InvalidOperationException("Failed to create user");
                }
                await userManager.AddToRoleAsync(user2, "User");
            }
            else
            {
                user2.Email = seedEmail2;
                user2.NormalizedEmail = seedEmail2.Normalize();

                user2.UserName = seedEmail2;
                user2.NormalizedUserName = seedEmail2.Normalize();

                await userManager.UpdateAsync(user2);
            }

            await context.SaveChangesAsync();

            context.ChangeTracker.Clear();

            await SeedMood(context);
            await SeedWellness(context);
        }

        private static async Task SeedMood(MoodAppDBContext context)
        {
            var user1 = await context.Users
                .FirstOrDefaultAsync(u => u.Email == "user1@gmail.com");

            var test2 = await context.Users
                .FirstOrDefaultAsync(u => u.Email == "test2@gmail.com");

            if (user1 == null || test2 == null)
            {
                throw new InvalidOperationException(
                    "Required users were not found.");
            }

            var templates = new List<Mood>
    {
        new Mood
        {
            UserId = user1.Id,
            Status = 6,
            Troubles = "Stressigt på jobbet",
            CreationDate = new DateTime(2026, 9, 7)
        },
        new Mood
        {
            UserId = user1.Id,
            Status = 8,
            Troubles = "",
            CreationDate = new DateTime(2026, 9, 8)
        },
        new Mood
        {
            UserId = user1.Id,
            Status = 4,
            Troubles = "Hårda deadlines på jobbet",
            CreationDate = new DateTime(2026, 9, 9)
        },
        new Mood
        {
            UserId = user1.Id,
            Status = 5,
            Troubles = "Fortfarande inte klar med deadlines",
            CreationDate = new DateTime(2026, 9, 10)
        },
        new Mood
        {
            UserId = user1.Id,
            Status = 5,
            Troubles = "Klar men är mentalt utmattad",
            CreationDate = new DateTime(2026, 9, 11)
        },

        new Mood
        {
            UserId = test2.Id,
            Status = 4,
            Troubles = "Många läxor",
            CreationDate = new DateTime(2026, 9, 7)
        },
        new Mood
        {
            UserId = test2.Id,
            Status = 5,
            Troubles = "Det är två prov imorgon",
            CreationDate = new DateTime(2026, 9, 8)
        },
        new Mood
        {
            UserId = test2.Id,
            Status = 5,
            Troubles = "Vet inte om något gick bra",
            CreationDate = new DateTime(2026, 9, 9)
        },
        new Mood
        {
            UserId = test2.Id,
            Status = 6,
            Troubles = "Hängde ut med mina kompisar",
            CreationDate = new DateTime(2026, 9, 10)
        },
        new Mood
        {
            UserId = test2.Id,
            Status = 7,
            Troubles = "Lättad av allt gick bra",
            CreationDate = new DateTime(2026, 9, 11)
        }
    };

            foreach (var mood in templates)
            {
                var exists = await context.Moods.FirstOrDefaultAsync(m =>
                    m.UserId == mood.UserId &&
                    m.Status == mood.Status &&
                    m.Troubles == mood.Troubles &&
                    m.CreationDate == mood.CreationDate);

                if (exists == null)
                {
                    context.Moods.Add(mood);
                }
            }

            await context.SaveChangesAsync();
        }


        private static async Task SeedWellness(MoodAppDBContext context)
        {
            // Get the users that the seeded wellness entries belong to
            var user1 = await context.Users
                .FirstOrDefaultAsync(u => u.Email == "user1@gmail.com");

            var test2 = await context.Users
                .FirstOrDefaultAsync(u => u.Email == "test2@gmail.com");

            // Make sure the users exist before creating wellness entries
            if (user1 == null || test2 == null)
            {
                throw new InvalidOperationException(
                    "Required users (user1@gmail.com, test2@gmail.com) were not found. " +
                    "Please ensure SeedUsers() has been called successfully.");
            }

            var templates = new List<Wellness>
    {
        // User 1
        new Wellness
        {
            UserId = user1.Id,
            Activity = "Quick workout",
            Food = "Chicken",
            SleepQuality = "",
            CreationDate = new DateTime(2026, 9, 7)
        },

        new Wellness
        {
            UserId = user1.Id,
            Activity = "Quick workout",
            Food = "Meatballs",
            SleepQuality = "Good",
            CreationDate = new DateTime(2026, 9, 8)
        },

        new Wellness
        {
            UserId = user1.Id,
            Activity = "Rest",
            Food = "Hamburger",
            SleepQuality = "",
            CreationDate = new DateTime(2026, 9, 9)
        },

        new Wellness
        {
            UserId = user1.Id,
            Activity = "Gym class",
            Food = "Salmon",
            SleepQuality = "",
            CreationDate = new DateTime(2026, 9, 10)
        },

        new Wellness
        {
            UserId = user1.Id,
            Activity = "Studying",
            Food = "Soup",
            SleepQuality = "Bad",
            CreationDate = new DateTime(2026, 9, 11)
        },

        // User 2
        new Wellness
        {
            UserId = test2.Id,
            Activity = "Studying",
            Food = "Soup",
            SleepQuality = "Bad",
            CreationDate = new DateTime(2026, 9, 12)
        },

        new Wellness
        {
            UserId = test2.Id,
            Activity = "Meditation",
            Food = "Salad",
            SleepQuality = "Good",
            CreationDate = new DateTime(2026, 9, 13)
        },

        new Wellness
        {
            UserId = test2.Id,
            Activity = "Reading",
            Food = "Fish",
            SleepQuality = "",
            CreationDate = new DateTime(2026, 9, 14)
        },

        new Wellness
        {
            UserId = test2.Id,
            Activity = "Gaming",
            Food = "Pizza",
            SleepQuality = "Average",
            CreationDate = new DateTime(2026, 9, 15)
        },

        new Wellness
        {
            UserId = test2.Id,
            Activity = "Walking",
            Food = "Fruits",
            SleepQuality = "Good",
            CreationDate = new DateTime(2026, 9, 16)
        }
    };

            // Add each wellness entry if it doesn't already exist
            foreach (var wellness in templates)
            {
                var exists = await context.Wellnesses
                    .FirstOrDefaultAsync(w =>
                        w.UserId == wellness.UserId &&
                        w.Activity == wellness.Activity &&
                        w.Food == wellness.Food &&
                        w.SleepQuality == wellness.SleepQuality &&
                        w.CreationDate == wellness.CreationDate);

                if (exists == null)
                {
                    context.Wellnesses.Add(wellness);
                }
            }

            await context.SaveChangesAsync();

            context.ChangeTracker.Clear();
        }
    }
}
