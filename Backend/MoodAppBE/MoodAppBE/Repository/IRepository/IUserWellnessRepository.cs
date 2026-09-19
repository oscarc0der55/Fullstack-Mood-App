using MoodAppBE.Models;

namespace MoodAppBE.Repository.IRepository
{
    public interface IUserWellnessRepository
    {
        Task<List<UsersWellness>> GetByUserIdAsync(int userId);

        Task<List<UsersWellness>> GetAllAsync();

        Task<UsersWellness?> GetByIdAsync(
            int usersWellnessId);

        Task<UsersWellness> CreateAsync(
            UsersWellness usersWellness);

        Task<bool> DeleteAsync(
            int usersWellnessId);
    }
}