using MoodAppBE.DTO.Wellness;

namespace MoodAppBE.Service.IService
{
    public interface IWellnessService
    {
        Task<List<WellnessDTO>> GetWellnessAsync();
        Task<WellnessDTO?> GetWellnessByIdAsync(int wellId);
        Task<WellnessDTO> CreateWellnessAsync(CreateWellnessDTO newWell);
        Task<bool> UpdateWellnessAsync(int wellId, UpdateWellnessDTO uWell);
        Task<bool> DeleteWellnessAsync(int wellId);
    }
}
