using MoodAppBE.Models;

namespace MoodAppBE.Repository.IRepository
{
    public interface IWellnessRepository
    {
        Task<List<Wellness>> GetWellnessAsync();
        Task<Wellness> GetWellnessByIdAsync(int wellId);
        Task<Wellness> CreateWellnessAsync(Wellness newWell);
        Task<bool> UpdateWellnessAsync(Wellness well);
        Task<bool> DeleteWellnessAsync(int wellId);
    }
}
