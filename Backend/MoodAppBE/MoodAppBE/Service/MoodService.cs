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
                Troubles = mood.Troubles
            }).ToList();

            return moodDTO;
        }

        public async Task<MoodDTO?> GetMoodByIdAsync(int moodId)
        {
            var mood = await moodRepository.GetMoodByIdAsync(moodId);
            if(mood == null)
            {
                return null;
            }

            var moodDTO = new MoodDTO
            {
                MoodId = mood.MoodId,
                Status = mood.Status,
                Troubles = mood.Troubles
            };

            return moodDTO;
        }

        public async Task<MoodDTO> CreateMoodAsync(CreateMoodDTO newMood)
        {
            
            var mood = new Mood
            {
                Status = newMood.Status,
                Troubles = newMood.Troubles
            };

            var createdMood = await moodRepository.CreateMoodAsync(mood);

            var moodDTO = new MoodDTO
            {
                Status = createdMood.Status,
                Troubles = createdMood.Troubles
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

        public async Task<bool> DeleteMovieAsync(int moodId)
        {
            return await moodRepository.DeleteMoodAsync(moodId);
        }
    }
}
