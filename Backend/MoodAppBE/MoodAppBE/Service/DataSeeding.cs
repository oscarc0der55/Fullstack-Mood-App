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
                var context = scope.ServiceProvider.GetRequiredService<MoodAppDBContext>();

                await SeedAdminUser(userManager, context);
                await SeedUsers(userManager ,context);
            }
        }
        private static async Task SeedAdminUser(UserManager<User> userManager, MoodAppDBContext context)
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
                var adminRoleExists = await userManager.AddToRoleAsync(newAdmin, "Admin");

                context.ChangeTracker.Clear();
                var existingSeedAdmin = await context.Users.FirstAsync(u => u.Email == "admin@mood.se");

                var adminProfile = new User
                {
                    Id = existingSeedAdmin.Id,
                    UserName = "admin@mood.se",
                    Email = "admin@mood.se",
                    EmailConfirmed = true
                };

                await context.Users.AddAsync(adminProfile);
            }
        }

        private static async Task SeedUsers(UserManager<User> userManager, MoodAppDBContext context)
        {
            var user1 = await context.Users.FirstOrDefaultAsync(u => u.SeedPos == 2);
            var user2 = await context.Users.FirstOrDefaultAsync(u => u.SeedPos == 3);

            const string seedEmail1 = "user1@gmail.com";
            const string seedEmail2 = "test2@gmail.com";

            if (user1 == null)
            {
                user1 = new User { UserName = seedEmail1, Email = seedEmail1, SeedPos = 2 };
                var result = userManager.CreateAsync(user1, "Password123!");

                if (result == null)
                {
                    throw new InvalidOperationException("Failed to create user");
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
                var result = userManager.CreateAsync(user2, "Password123!");

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

        private static async Task <List<Mood>> SeedMood(MoodAppDBContext context)
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

            await context.SaveChangesAsync();
            var createdMoods = new List<Mood>();
            foreach (var t in templates)
            {
                var m = await context.Moods.FirstOrDefaultAsync(m => m.MoodId == t.MoodId);
                if (m != null)
                {
                    createdMoods.Add(m);
                }
            }
            return createdMoods;
        }

        private static async Task <List<Wellness>> SeedWellness(MoodAppDBContext context)
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
                }
            };

            await context.SaveChangesAsync();

            var createdWell = new List<Wellness>();
            foreach (var t in templates)
            {
                var w = await context.Wellnesses.FirstOrDefaultAsync(w => w.WellnessId == t.WellnessId);
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

            var moods = await context.Moods.ToListAsync();
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

                if (exists != null)
                {

                }
                else
                {
                    await context.UsersMoods.AddAsync(t);
                }
            }
            await context.SaveChangesAsync();
        }
        private static async Task SeedUserWellness(MoodAppDBContext context)
        {
            var user1 = await context.Users.FirstOrDefaultAsync(u => u.Email == "user1@gmail.com");
            var test2 = await context.Users.FirstOrDefaultAsync(t => t.Email == "test2@gmail.com");

            var wells = await context.Wellnesses.ToListAsync();
            var templates = new List<UsersWellness>()
                {
                new UsersWellness
                {
                    Id = user1.Id,
                    WellnessId = wells[0].WellnessId,
                    CreationDate = new DateTime(2026, 9, 7)
                },
                new UsersWellness
                {
                    Id = user1.Id,
                    WellnessId = wells[1].WellnessId,
                    CreationDate = new DateTime(2026, 9, 8)
                },
                new UsersWellness
                {
                    Id = user1.Id,
                    WellnessId = wells[2].WellnessId,
                    CreationDate = new DateTime(2026, 9, 9)
                },
                new UsersWellness
                {
                    Id = test2.Id,
                    WellnessId = wells[3].WellnessId,
                    CreationDate = new DateTime(2026, 9, 7)
                },
                new UsersWellness
                {
                    Id = test2.Id,
                    WellnessId = wells[4].WellnessId,
                    CreationDate = new DateTime(2026, 9, 8)
                },
                new UsersWellness
                {
                    Id = test2.Id,
                    WellnessId = wells[5].WellnessId,
                    CreationDate = new DateTime(2026, 9, 9)
                }
            };

            foreach(var t in templates)
            {
                var exists = await context.UsersWellnesses.FirstOrDefaultAsync(u => u.Id == t.Id && u.WellnessId == t.WellnessId && u.CreationDate == t.CreationDate);
                if (exists != null)
                {

                }
                else
                {
                    await context.UsersWellnesses.AddAsync(t);
                }
            }
            await context.SaveChangesAsync();
        }
    }
}
