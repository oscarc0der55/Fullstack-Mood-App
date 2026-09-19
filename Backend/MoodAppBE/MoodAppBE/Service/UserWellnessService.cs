using MoodAppBE.DTO.UserWellness;
using MoodAppBE.Models;
using MoodAppBE.Repository.IRepository;
using MoodAppBE.Service.IService;

namespace MoodAppBE.Service
{
    public class UserWellnessService : IUserWellnessService
    {
        private readonly IUserWellnessRepository
            userWellnessRepository;

        public UserWellnessService(
            IUserWellnessRepository userWellnessRepository)
        {
            this.userWellnessRepository =
                userWellnessRepository;
        }

        public async Task<List<UserWellnessDTO>> GetByUserIdAsync(
            int userId)
        {
            var wellnessRecords =
                await userWellnessRepository
                    .GetByUserIdAsync(userId);

            return wellnessRecords
                .Select(MapToDTO)
                .ToList();
        }

        public async Task<List<UserWellnessDTO>> GetAllAsync()
        {
            var wellnessRecords =
                await userWellnessRepository.GetAllAsync();

            return wellnessRecords
                .Select(MapToDTO)
                .ToList();
        }

        public async Task<UserWellnessDTO?> GetByIdAsync(
            int usersWellnessId)
        {
            var wellness =
                await userWellnessRepository
                    .GetByIdAsync(usersWellnessId);

            if (wellness == null)
            {
                return null;
            }

            return MapToDTO(wellness);
        }

        public async Task<UserWellnessDTO> CreateAsync(
            int userId,
            int wellnessId)
        {
            var usersWellness = new UsersWellness
            {
                Id = userId,
                WellnessId = wellnessId,
                CreationDate = DateTime.UtcNow
            };

            var createdWellness =
                await userWellnessRepository
                    .CreateAsync(usersWellness);

            var completeWellness =
                await userWellnessRepository.GetByIdAsync(
                    createdWellness.UsersWellnessId);

            return MapToDTO(completeWellness!);
        }

        public async Task<bool> DeleteAsync(
            int usersWellnessId,
            int userId,
            bool isAdmin)
        {
            var wellness =
                await userWellnessRepository
                    .GetByIdAsync(usersWellnessId);

            if (wellness == null)
            {
                return false;
            }

            if (!isAdmin && wellness.Id != userId)
            {
                return false;
            }

            return await userWellnessRepository
                .DeleteAsync(usersWellnessId);
        }

        private static UserWellnessDTO MapToDTO(
            UsersWellness usersWellness)
        {
            return new UserWellnessDTO
            {
                UsersWellnessId =
                    usersWellness.UsersWellnessId,

                UserId = usersWellness.Id,
                UserName = usersWellness.User?.Name,

                WellnessId = usersWellness.WellnessId,
                Activity = usersWellness.Wellness.Activity,
                Food = usersWellness.Wellness.Food,
                SleepQuality =
                    usersWellness.Wellness.SleepQuality,

                CreationDate = usersWellness.CreationDate
            };
        }
    }
}