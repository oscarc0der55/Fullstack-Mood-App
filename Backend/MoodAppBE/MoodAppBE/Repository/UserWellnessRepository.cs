using Microsoft.EntityFrameworkCore;
using MoodAppBE.Data;
using MoodAppBE.Models;
using MoodAppBE.Repository.IRepository;

namespace MoodAppBE.Repository
{
    public class UserWellnessRepository : IUserWellnessRepository
    {
        private readonly MoodAppDBContext context;

        public UserWellnessRepository(MoodAppDBContext context)
        {
            this.context = context;
        }

        public async Task<List<UsersWellness>> GetByUserIdAsync(
            int userId)
        {
            return await context.UsersWellnesses
                .AsNoTracking()
                .Include(userWellness => userWellness.User)
                .Include(userWellness => userWellness.Wellness)
                .Where(userWellness =>
                    userWellness.Id == userId)
                .OrderBy(userWellness =>
                    userWellness.CreationDate)
                .ToListAsync();
        }

        public async Task<List<UsersWellness>> GetAllAsync()
        {
            return await context.UsersWellnesses
                .AsNoTracking()
                .Include(userWellness => userWellness.User)
                .Include(userWellness => userWellness.Wellness)
                .OrderBy(userWellness =>
                    userWellness.CreationDate)
                .ToListAsync();
        }

        public async Task<UsersWellness?> GetByIdAsync(
            int usersWellnessId)
        {
            return await context.UsersWellnesses
                .Include(userWellness => userWellness.User)
                .Include(userWellness => userWellness.Wellness)
                .FirstOrDefaultAsync(userWellness =>
                    userWellness.UsersWellnessId ==
                    usersWellnessId);
        }

        public async Task<UsersWellness> CreateAsync(
            UsersWellness usersWellness)
        {
            context.UsersWellnesses.Add(usersWellness);

            await context.SaveChangesAsync();

            return usersWellness;
        }

        public async Task<bool> DeleteAsync(
            int usersWellnessId)
        {
            var rowsAffected = await context.UsersWellnesses
                .Where(userWellness =>
                    userWellness.UsersWellnessId ==
                    usersWellnessId)
                .ExecuteDeleteAsync();

            return rowsAffected > 0;
        }
    }
}