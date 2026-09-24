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

            // Configure UsersMood entity
            modelBuilder.Entity<UsersMood>()
                .HasKey(um => um.UsersMoodId);

            modelBuilder.Entity<UsersMood>()
                .Property(um => um.UsersMoodId)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<UsersMood>()
                .HasOne(um => um.User)
                .WithMany()
                .HasForeignKey(um => um.Id);

            // Configure UsersWellness entity
            modelBuilder.Entity<UsersWellness>()
                .HasKey(uw => uw.UsersWellnessId);

            modelBuilder.Entity<UsersWellness>()
                .Property(uw => uw.UsersWellnessId)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<UsersWellness>()
                .HasOne(uw => uw.User)
                .WithMany()
                .HasForeignKey(uw => uw.Id);

            modelBuilder.Entity<IdentityRole<int>>().HasData(
                new IdentityRole<int>
                {
                    Id = 1,
                    Name = "OriginalUser",
                    NormalizedName = "OGUSER123",
                    ConcurrencyStamp = ""
                });
        }

    }
}
