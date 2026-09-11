using MoodAppBE.DTO.Mood;

namespace MoodAppBE.Service.IService
{
    public interface IMoodService
    {
        Task<List<MoodDTO>> GetMoodsAsync();
        Task<MoodDTO?> GetMoodByIdAsync(int moodId);
        Task<MoodDTO> CreateMoodAsync(CreateMoodDTO newMood);
        Task<bool> UpdateMoodAsync(int moodId, UpdateMoodDTO uMood);
        Task<bool> DeleteMovieAsync(int moodId);
    }
}
