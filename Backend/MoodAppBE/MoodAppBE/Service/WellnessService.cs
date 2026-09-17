using MoodAppBE.DTO.Wellness;
using MoodAppBE.Models;
using MoodAppBE.Repository;
using MoodAppBE.Repository.IRepository;
using MoodAppBE.Service.IService;

namespace MoodAppBE.Service
{
    public class WellnessService : IWellnessService
    {
        private readonly IWellnessRepository wellnessRepository;
        public WellnessService(IWellnessRepository _wellnessRepository)
        {
            wellnessRepository = _wellnessRepository;
        }
        public async Task<List<WellnessDTO>> GetWellnessAsync()
        {
            var wells = await wellnessRepository.GetWellnessAsync();

            var wellDTO = wells.Select(well => new WellnessDTO
            {
                WellnessId = well.WellnessId,
                Activity = well.Activity,
                Food = well.Food,
                SleepQuality = well.SleepQuality

            }).ToList();

            return wellDTO;
        }

        public async Task<WellnessDTO?> GetWellnessByIdAsync(int wellId)
        {
            var well = await wellnessRepository.GetWellnessByIdAsync(wellId);
            if (well == null)
            {
                return null;
            }

            var wellDTO = new WellnessDTO
            {
                WellnessId = well.WellnessId,
                Activity = well.Activity,
                Food = well.Food,
                SleepQuality = well.SleepQuality
            };

            return wellDTO;
        }

        public async Task<WellnessDTO> CreateWellnessAsync(CreateWellnessDTO newWell)
        {

            var well = new Wellness
            {
                Activity = newWell.Activity,
                Food = newWell.Food,
                SleepQuality = newWell.SleepQuality
            };

            var createdWell = await wellnessRepository.CreateWellnessAsync(well);

            var WellnessDTO = new WellnessDTO
            {
                Activity = createdWell.Activity,
                Food = createdWell.Food,
                SleepQuality = createdWell.SleepQuality
            };

            return WellnessDTO;
        }

        public async Task<bool> UpdateWellnessAsync(int wellId, UpdateWellnessDTO uWell)
        {
            var existingWell = await wellnessRepository.GetWellnessByIdAsync(wellId);

            if (existingWell == null)
            {
                return false;
            }

            existingWell.Activity = uWell.Activity;
            existingWell.Food = uWell.Food;
            existingWell.SleepQuality = uWell.SleepQuality;

            return await wellnessRepository.UpdateWellnessAsync(existingWell);
        }

        public async Task<bool> DeleteWellnessAsync(int wellId)
        {
            return await wellnessRepository.DeleteWellnessAsync(wellId);
        }
    }
}
