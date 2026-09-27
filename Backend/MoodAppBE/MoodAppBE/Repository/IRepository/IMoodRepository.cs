using MoodAppBE.Models;

namespace MoodAppBE.Repository.IRepository
{
    public interface IMoodRepository
    {
        Task<List<Mood>> GetMoodsAsync();
        Task<List<Mood>> GetMoodsByUserIdAsync(int userId);
        Task<Mood> GetMoodByIdAsync(int moodId);
        Task<Mood> CreateMoodAsync(Mood newMood);
        Task<bool> UpdateMoodAsync(Mood mood);
        Task<bool> DeleteMoodAsync(int moodId);
    }
}
