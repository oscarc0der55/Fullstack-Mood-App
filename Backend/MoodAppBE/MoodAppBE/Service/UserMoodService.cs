using MoodAppBE.DTO.UserMood;
using MoodAppBE.Models;
using MoodAppBE.Repository.IRepository;
using MoodAppBE.Service.IService;

namespace MoodAppBE.Service
{
    public class UserMoodService : IUserMoodService
    {
        private readonly IUserMoodRepository userMoodRepository;

        public UserMoodService(IUserMoodRepository userMoodRepository)
        {
            this.userMoodRepository = userMoodRepository;
        }

        public async Task<List<UserMoodDTO>> GetByUserIdAsync(int userId)
        {
            var userMoods =
                await userMoodRepository.GetByUserIdAsync(userId);

            return userMoods
                .Select(MapToDTO)
                .ToList();
        }

        public async Task<List<UserMoodDTO>> GetAllAsync()
        {
            var userMoods =
                await userMoodRepository.GetAllAsync();

            return userMoods
                .Select(MapToDTO)
                .ToList();
        }

        public async Task<UserMoodDTO?> GetByIdAsync(
            int usersMoodId)
        {
            var userMood =
                await userMoodRepository.GetByIdAsync(usersMoodId);

            if (userMood == null)
            {
                return null;
            }

            return MapToDTO(userMood);
        }

        public async Task<UserMoodDTO> CreateAsync(
            int userId,
            int moodId)
        {
            var userMood = new UsersMood
            {
                Id = userId,
                MoodId = moodId,
                CreationDate = DateTime.UtcNow
            };

            var createdUserMood =
                await userMoodRepository.CreateAsync(userMood);

            var completeUserMood =
                await userMoodRepository.GetByIdAsync(
                    createdUserMood.UsersMoodId);

            return MapToDTO(completeUserMood!);
        }

        public async Task<bool> DeleteAsync(
            int usersMoodId,
            int userId,
            bool isAdmin)
        {
            var userMood =
                await userMoodRepository.GetByIdAsync(usersMoodId);

            if (userMood == null)
            {
                return false;
            }

            // Users may delete only their own records.
            // Admins may delete any record.
            if (!isAdmin && userMood.Id != userId)
            {
                return false;
            }

            return await userMoodRepository.DeleteAsync(usersMoodId);
        }

        private static UserMoodDTO MapToDTO(UsersMood userMood)
        {
            return new UserMoodDTO
            {
                UsersMoodId = userMood.UsersMoodId,
                UserId = userMood.Id,
                UserName = userMood.User?.Name,
                MoodId = userMood.MoodId,

                Status = userMood.Mood.Status,
                Troubles = userMood.Mood.Troubles,

                CreationDate = userMood.CreationDate
            };
        }
    }
}