using MoodAppBE.DTO.Mood;
using MoodAppBE.Repository.IRepository;
using MoodAppBE.Service.IService;
using MoodAppBE.Models;

namespace MoodAppBE.Service
{
    public class MoodService : IMoodService
    {
        private readonly IMoodRepository moodRepository;

        public MoodService(IMoodRepository _moodRepository)
        {
            moodRepository = _moodRepository;
        }

        public async Task<List<MoodDTO>> GetMoodsAsync()
        {
            var moods = await moodRepository.GetMoodsAsync();

            var moodDTO = moods.Select(mood => new MoodDTO
            {
                MoodId = mood.MoodId,
                Status = mood.Status,
                Troubles = mood.Troubles,
                CreationDate = mood.CreationDate,
                UserId = mood.UserId
            }).ToList();

            return moodDTO;
        }

        public async Task<List<MoodDTO>> GetMoodsByUserIdAsync(int userId)
        {
            var moods = await moodRepository.GetMoodsByUserIdAsync(userId);

            var moodDTO = moods.Select(mood => new MoodDTO
            {
                MoodId = mood.MoodId,
                Status = mood.Status,
                Troubles = mood.Troubles,
                CreationDate = mood.CreationDate,
                UserId = mood.UserId
            }).ToList();

            return moodDTO;
        }

        public async Task<MoodDTO?> GetMoodByIdAsync(int moodId)
        {
            var mood = await moodRepository.GetMoodByIdAsync(moodId);
            if (mood == null)
            {
                return null;
            }

            var moodDTO = new MoodDTO
            {
                MoodId = mood.MoodId,
                Status = mood.Status,
                Troubles = mood.Troubles,
                CreationDate = mood.CreationDate,
                UserId = mood.UserId
            };

            return moodDTO;
        }

        public async Task<MoodDTO> CreateMoodAsync(int userId, CreateMoodDTO newMood)
        {
            var mood = new Mood
            {
                Status = newMood.Status,
                Troubles = newMood.Troubles,
                UserId = userId,
                CreationDate = DateTime.UtcNow
            };

            var createdMood = await moodRepository.CreateMoodAsync(mood);

            var moodDTO = new MoodDTO
            {
                MoodId = createdMood.MoodId,
                Status = createdMood.Status,
                Troubles = createdMood.Troubles,
                CreationDate = createdMood.CreationDate,
                UserId = createdMood.UserId
            };

            return moodDTO;
        }

        public async Task<bool> UpdateMoodAsync(int moodId, UpdateMoodDTO uMood)
        {
            var existingMood = await moodRepository.GetMoodByIdAsync(moodId);

            if (existingMood == null)
            {
                return false;
            }

            existingMood.Status = uMood.Status;
            existingMood.Troubles = uMood.Troubles;

            return await moodRepository.UpdateMoodAsync(existingMood);
        }

        public async Task<bool> DeleteMoodAsync(int moodId)
        {
            return await moodRepository.DeleteMoodAsync(moodId);
        }
    }
}
