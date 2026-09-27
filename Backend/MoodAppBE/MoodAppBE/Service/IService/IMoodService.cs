using MoodAppBE.DTO.Mood;

namespace MoodAppBE.Service.IService
{
    public interface IMoodService
    {
        Task<List<MoodDTO>> GetMoodsAsync();
        Task<List<MoodDTO>> GetMoodsByUserIdAsync(int userId);
        Task<MoodDTO?> GetMoodByIdAsync(int moodId);
        Task<MoodDTO> CreateMoodAsync(int userId, CreateMoodDTO newMood);
        Task<bool> UpdateMoodAsync(int moodId, UpdateMoodDTO uMood);
        Task<bool> DeleteMoodAsync(int moodId);
    }
}
