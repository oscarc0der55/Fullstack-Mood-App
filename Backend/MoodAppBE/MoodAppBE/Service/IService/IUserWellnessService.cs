using MoodAppBE.DTO.UserWellness;

namespace MoodAppBE.Service.IService
{
    public interface IUserWellnessService
    {
        Task<List<UserWellnessDTO>> GetByUserIdAsync(int userId);

        Task<List<UserWellnessDTO>> GetAllAsync();

        Task<UserWellnessDTO?> GetByIdAsync(int usersWellnessId);

        Task<UserWellnessDTO> CreateAsync(int userId, int wellnessId);

        Task<bool> DeleteAsync(int usersWellnessId, int userId, bool isAdmin);
    }
}