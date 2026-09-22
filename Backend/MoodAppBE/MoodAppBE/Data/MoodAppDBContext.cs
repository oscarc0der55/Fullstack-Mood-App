using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MoodAppBE.Models;

namespace MoodAppBE.Data
{
    public class MoodAppDBContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public MoodAppDBContext(DbContextOptions<MoodAppDBContext> options) : base(options)
        {

        }

        public DbSet<Mood> Moods { get; set; }
        public DbSet<Wellness> Wellnesses { get; set; }
        public DbSet<UsersMood> UsersMoods { get; set; }
        public DbSet<UsersWellness> UsersWellnesses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UsersMood>()
                .HasKey(um => um.UsersMoodId);

            modelBuilder.Entity<UsersWellness>().HasKey(uw => uw.UsersWellnessId);

            modelBuilder.Entity<IdentityRole<int>>().HasData(
                new IdentityRole<int>
                {
                    Id = 1,
                    Name = "User1",
                    NormalizedName = "USER123",
                    ConcurrencyStamp = ""
                });
        }

    }
}
