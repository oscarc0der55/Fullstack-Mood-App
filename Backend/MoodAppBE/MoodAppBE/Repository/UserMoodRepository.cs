using Microsoft.EntityFrameworkCore;
using MoodAppBE.Data;
using MoodAppBE.Models;
using MoodAppBE.Repository.IRepository;

namespace MoodAppBE.Repository
{
    public class UserMoodRepository : IUserMoodRepository
    {
        private readonly MoodAppDBContext context;

        public UserMoodRepository(MoodAppDBContext context)
        {
            this.context = context;
        }

        public async Task<List<UsersMood>> GetByUserIdAsync(
            int userId)
        {
            return await context.UsersMoods
                .AsNoTracking()
                .Include(userMood => userMood.User)
                .Include(userMood => userMood.Mood)
                .Where(userMood => userMood.Id == userId)
                .OrderBy(userMood => userMood.CreationDate)
                .ToListAsync();
        }

        public async Task<List<UsersMood>> GetAllAsync()
        {
            return await context.UsersMoods
                .AsNoTracking()
                .Include(userMood => userMood.User)
                .Include(userMood => userMood.Mood)
                .OrderBy(userMood => userMood.CreationDate)
                .ToListAsync();
        }

        public async Task<UsersMood?> GetByIdAsync(
            int usersMoodId)
        {
            return await context.UsersMoods
                .Include(userMood => userMood.User)
                .Include(userMood => userMood.Mood)
                .FirstOrDefaultAsync(
                    userMood =>
                        userMood.UsersMoodId == usersMoodId);
        }

        public async Task<UsersMood> CreateAsync(
            UsersMood userMood)
        {
            context.UsersMoods.Add(userMood);

            await context.SaveChangesAsync();

            return userMood;
        }

        public async Task<bool> DeleteAsync(
            int usersMoodId)
        {
            var rowsAffected = await context.UsersMoods
                .Where(userMood =>
                    userMood.UsersMoodId == usersMoodId)
                .ExecuteDeleteAsync();

            return rowsAffected > 0;
        }
    }
}