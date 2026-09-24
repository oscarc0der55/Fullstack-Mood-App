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
            await SeedUserMoods(context);
            await SeedUserWellness(context);
        }

        private static async Task SeedMood(MoodAppDBContext context)
        {
            var templates = new List<Mood>()
            {
                new Mood
                {
                    Status = 6,
                    Troubles = "Stressigt på jobbet"
                },
                new Mood
                {
                    Status = 8,
                    Troubles = ""
                },
                 new Mood
                {
                    Status = 4,
                    Troubles = "Hårda deadlines på jobbet"
                },
                  new Mood
                {
                    Status = 5,
                    Troubles = "Fortfarande inte klar med deadlines"
                },
                   new Mood
                {
                    Status = 5,
                    Troubles = "Klar men är mentalt utmattad"
                },
                   new Mood
                {
                    Status = 4,
                    Troubles = "Många läxor"
                },
                new Mood
                {
                    Status = 5,
                    Troubles = "Det är två prov imorgon"
                },
                 new Mood
                {
                    Status = 5,
                    Troubles = "Vet inte om något gick bra"
                },
                  new Mood
                {
                    Status = 6,
                    Troubles = "Hängde ut med mina kompisar"
                },
                   new Mood
                {
                    Status = 7,
                    Troubles = "Lättad av allt gick bra"
                }
            };

            // Add moods to the context
            foreach (var mood in templates)
            {
                var exists = await context.Moods.FirstOrDefaultAsync(m => m.Status == mood.Status && m.Troubles == mood.Troubles);
                if (exists == null)
                {
                    await context.Moods.AddAsync(mood);
                }
            }

            await context.SaveChangesAsync();
        }

        private static async Task<List<Wellness>> SeedWellness(MoodAppDBContext context)
        {
            var templates = new List<Wellness>()
            {
                new Wellness
                {
                    Activity = "Quick workout",
                    Food = "Chicken",
                    SleepQuality = ""
                },
                new Wellness
                {
                    Activity = "Quick workout",
                    Food = "Meatballs",
                    SleepQuality = "Good"
                },
                new Wellness
                {
                    Activity = "Rest",
                    Food = "Hamburger",
                    SleepQuality = ""
                },
                new Wellness
                {
                    Activity = "Gym class",
                    Food = "Salmon",
                    SleepQuality = ""
                },
                new Wellness
                {
                    Activity = "Studying",
                    Food = "Soup",
                    SleepQuality = "Bad"
                },
                new Wellness
                {
                    Activity = "Studying",
                    Food = "Soup",
                    SleepQuality = "Bad"
                },
                new Wellness
                {
                    Activity = "Meditation",
                    Food = "Salad",
                    SleepQuality = "Good"
                },
                new Wellness
                {
                    Activity = "Reading",
                    Food = "Fish",
                    SleepQuality = ""
                },
                new Wellness
                {
                    Activity = "Gaming",
                    Food = "Pizza",
                    SleepQuality = "Average"
                },
                new Wellness
                {
                    Activity = "Walking",
                    Food = "Fruits",
                    SleepQuality = "Good"
                }
            };

            // Add wellnesses to the context
            foreach (var t in templates)
            {
                var exists = await context.Wellnesses.FirstOrDefaultAsync(w => w.Activity == t.Activity && w.Food == t.Food && w.SleepQuality == t.SleepQuality);
                if (exists == null)
                {
                    context.Wellnesses.Add(t);
                }
            }

            await context.SaveChangesAsync();

            var createdWell = new List<Wellness>();
            foreach (var t in templates)
            {
                var w = await context.Wellnesses.FirstOrDefaultAsync(w => w.Activity == t.Activity && w.Food == t.Food && w.SleepQuality == t.SleepQuality);
                if (w != null)
                {
                    createdWell.Add(w);
                }
            }
            return createdWell;
        }

        private static async Task SeedUserMoods(MoodAppDBContext context)
        {
            var user1 = await context.Users.FirstOrDefaultAsync(u => u.Email == "user1@gmail.com");
            var test2 = await context.Users.FirstOrDefaultAsync(t => t.Email == "test2@gmail.com");

            if (user1 == null || test2 == null)
            {
                throw new InvalidOperationException("Required users (user1@gmail.com, test2@gmail.com) were not found. Please ensure SeedUsers() has been called successfully.");
            }

            var moods = await context.Moods.ToListAsync();

            // Validate that we have enough moods seeded
            if (moods.Count < 10)
            {
                throw new InvalidOperationException($"Expected at least 10 moods to be seeded, but found only {moods.Count}. Please ensure SeedMood() has been called successfully.");
            }

            var templates = new List<UsersMood>()
            {
                new UsersMood
                {
                    Id = user1.Id,
                    MoodId = moods[0].MoodId,
                    CreationDate = new DateTime(2026, 9, 7)
                },
                 new UsersMood
                {
                    Id = user1.Id,
                    MoodId = moods[1].MoodId,
                    CreationDate = new DateTime(2026, 9, 8)
                },
                new UsersMood
                {
                    Id = user1.Id,
                    MoodId = moods[2].MoodId,
                    CreationDate = new DateTime(2026, 9, 9)
                },
                new UsersMood
                {
                    Id = user1.Id,
                    MoodId = moods[3].MoodId,
                    CreationDate = new DateTime(2026, 9, 10)
                },
                new UsersMood
                {
                   Id = user1.Id,
                   MoodId = moods[4].MoodId,
                   CreationDate = new DateTime(2026, 9, 11)
                },
                new UsersMood
                {
                    Id = test2.Id,
                    MoodId = moods[5].MoodId,
                    CreationDate = new DateTime(2026, 9, 7)
                },
                new UsersMood
                {
                    Id = test2.Id,
                    MoodId = moods[6].MoodId,
                    CreationDate = new DateTime(2026, 9, 8)
                },
                new UsersMood
                {
                    Id = test2.Id,
                    MoodId = moods[7].MoodId,
                    CreationDate = new DateTime(2026, 9, 9)
                },
                new UsersMood
                {
                    Id = test2.Id,
                    MoodId = moods[8].MoodId,
                    CreationDate = new DateTime(2026, 9, 10)
                },
                new UsersMood
                {
                    Id = test2.Id,
                    MoodId = moods[9].MoodId,
                    CreationDate = new DateTime(2026, 9, 11)
                }
            };

            foreach (var t in templates)
            {
                var exists = await context.UsersMoods.FirstOrDefaultAsync(u => u.Id == t.Id && u.MoodId == t.MoodId && u.CreationDate == t.CreationDate);

                if (exists == null)
                {
                    context.UsersMoods.Add(t);
                }
            }

            await context.SaveChangesAsync();
            context.ChangeTracker.Clear();
        }
        private static async Task SeedUserWellness(MoodAppDBContext context)
        {
            var user1 = await context.Users.FirstOrDefaultAsync(u => u.Email == "user1@gmail.com");
            var test2 = await context.Users.FirstOrDefaultAsync(t => t.Email == "test2@gmail.com");

            if (user1 == null || test2 == null)
            {
                throw new InvalidOperationException("Required users (user1@gmail.com, test2@gmail.com) were not found. Please ensure SeedUsers() has been called successfully.");
            }

            var wellnesses = await context.Wellnesses.ToListAsync();

            // Validate that we have enough wellnesses seeded
            if (wellnesses.Count < 10)
            {
                throw new InvalidOperationException($"Expected at least 10 wellnesses to be seeded, but found only {wellnesses.Count}. Please ensure SeedWellness() has been called successfully.");
            }

            var templates = new List<UsersWellness>()
    {
        new UsersWellness
        {
            Id = user1.Id,
            WellnessId = wellnesses[0].WellnessId,
            CreationDate = new DateTime(2026, 9, 7)
        },
        new UsersWellness
        {
            Id = user1.Id,
            WellnessId = wellnesses[1].WellnessId,
            CreationDate = new DateTime(2026, 9, 8)
        },
        new UsersWellness
        {
            Id = user1.Id,
            WellnessId = wellnesses[2].WellnessId,
            CreationDate = new DateTime(2026, 9, 9)
        },
        new UsersWellness
        {
            Id = user1.Id,
            WellnessId = wellnesses[3].WellnessId,
            CreationDate = new DateTime(2026, 9, 10)
        },
        new UsersWellness
        {
            Id = user1.Id,
            WellnessId = wellnesses[4].WellnessId,
            CreationDate = new DateTime(2026, 9, 11)
        },
        new UsersWellness
        {
            Id = test2.Id,
            WellnessId = wellnesses[5].WellnessId,
            CreationDate = new DateTime(2026, 9, 12)
        },
        new UsersWellness
        {
            Id = test2.Id,
            WellnessId = wellnesses[6].WellnessId,
            CreationDate = new DateTime(2026, 9, 13)
        },
        new UsersWellness
        {
            Id = test2.Id,
            WellnessId = wellnesses[7].WellnessId,
            CreationDate = new DateTime(2026, 9, 14)
        },
        new UsersWellness
        {
            Id = test2.Id,
            WellnessId = wellnesses[8].WellnessId,
            CreationDate = new DateTime(2026, 9, 15)
        },
        new UsersWellness
        {
            Id = test2.Id,
            WellnessId = wellnesses[9].WellnessId,
            CreationDate = new DateTime(2026, 9, 16)
        }
    };

            foreach (var t in templates)
            {
                var exists = await context.UsersWellnesses.FirstOrDefaultAsync(u => u.Id == t.Id && u.WellnessId == t.WellnessId && u.CreationDate == t.CreationDate);

                if (exists == null)
                {
                    context.UsersWellnesses.Add(t);
                }
            }

            await context.SaveChangesAsync();
            context.ChangeTracker.Clear();
        }
    }
}
