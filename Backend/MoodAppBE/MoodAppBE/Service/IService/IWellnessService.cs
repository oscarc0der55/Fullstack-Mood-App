using MoodAppBE.DTO.Wellness;

namespace MoodAppBE.Service.IService
{
    public interface IWellnessService
    {
        Task<List<WellnessDTO>> GetWellnessAsync();
        Task<List<WellnessDTO>> GetWellnessByUserIdAsync(int userId);
        Task<WellnessDTO?> GetWellnessByIdAsync(int wellId);
        Task<WellnessDTO> CreateWellnessAsync(int userId, CreateWellnessDTO newWell);
        Task<bool> UpdateWellnessAsync(int wellId, UpdateWellnessDTO uWell);
        Task<bool> DeleteWellnessAsync(int wellId);
    }
}
