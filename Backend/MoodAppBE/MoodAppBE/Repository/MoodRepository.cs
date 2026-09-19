using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileSystemGlobbing.Internal.PatternContexts;
using MoodAppBE.Data;
using MoodAppBE.Models;
using MoodAppBE.Repository.IRepository;

namespace MoodAppBE.Repository
{
    public class MoodRepository : IMoodRepository
    {
        private readonly MoodAppDBContext context;

        public MoodRepository(MoodAppDBContext _context)
        {
            context = _context;
        }

        public async Task<List<Mood>> GetMoodsAsync()
        {
            var moods = await context.Moods.AsNoTracking().ToListAsync();
            return moods;
        }
        public async Task<Mood> GetMoodByIdAsync(int moodId)
        {
            //Mood may be null, add error handling
            var mood = await context.Moods.FirstOrDefaultAsync(m => m.MoodId == moodId);
            return mood;
        }
        public async Task<Mood> CreateMoodAsync(Mood newMood)
        {
            //Look how you did during CC
            context.Moods.Add(newMood);
            await context.SaveChangesAsync();

            return newMood;
        }
        public async Task<bool> UpdateMoodAsync(Mood mood)
        {
            context.Moods.Update(mood);
            var result = await context.SaveChangesAsync();

            if (result > 0)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteMoodAsync(int moodId)
        {
            var rowsAffected = await context.Moods.Where(m => m.MoodId == moodId).ExecuteDeleteAsync();

            if (rowsAffected > 0)
            {
                return true;
            }

            return false;
        }
    }
}
