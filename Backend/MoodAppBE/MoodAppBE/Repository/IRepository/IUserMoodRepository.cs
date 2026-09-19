using MoodAppBE.Models;

namespace MoodAppBE.Repository.IRepository
{
    public interface IUserMoodRepository
    {
        Task<List<UsersMood>> GetByUserIdAsync(int userId);

        Task<List<UsersMood>> GetAllAsync();

        Task<UsersMood?> GetByIdAsync(int usersMoodId);

        Task<UsersMood> CreateAsync(UsersMood userMood);

        Task<bool> DeleteAsync(int usersMoodId);
    }
}