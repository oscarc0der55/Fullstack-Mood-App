using MoodAppBE.DTO.UserMood;

namespace MoodAppBE.Service.IService
{
    public interface IUserMoodService
    {
        Task<List<UserMoodDTO>> GetByUserIdAsync(int userId);
        Task<List<UserMoodDTO>> GetAllAsync();
        Task<UserMoodDTO?> GetByIdAsync(int usersMoodId);
        Task<UserMoodDTO> CreateAsync(int userId, int moodId);
        Task<bool> DeleteAsync(
            int usersMoodId,
            int userId,
            bool isAdmin);
    }
}