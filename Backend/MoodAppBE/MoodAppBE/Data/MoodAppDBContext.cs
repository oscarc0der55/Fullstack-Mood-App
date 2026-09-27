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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Mood entity
            modelBuilder.Entity<Mood>()
                .HasOne(m => m.User)
                .WithMany()
                .HasForeignKey(m => m.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure Wellness entity
            modelBuilder.Entity<Wellness>()
                .HasOne(w => w.User)
                .WithMany()
                .HasForeignKey(w => w.UserId)
                .OnDelete(DeleteBehavior.Cascade);

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
