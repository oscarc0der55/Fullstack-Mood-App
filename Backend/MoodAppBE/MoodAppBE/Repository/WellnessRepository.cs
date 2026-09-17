using Microsoft.EntityFrameworkCore;
using MoodAppBE.Data;
using MoodAppBE.Models;
using MoodAppBE.Repository.IRepository;

namespace MoodAppBE.Repository
{
    public class WellnessRepository : IWellnessRepository
    {
        private readonly MoodAppDBContext context;
        public WellnessRepository(MoodAppDBContext _context)
        {
            context = _context;
        }
        public async Task<List<Wellness>> GetWellnessAsync()
        {
            var wells = await context.Wellnesses.AsNoTracking().ToListAsync();
            return wells;
        }
        public async Task<Wellness> GetWellnessByIdAsync(int wellId)
        {
            //Mood may be null, add error handling
            var wellness = await context.Wellnesses.FirstOrDefaultAsync(w => w.WellnessId == wellId);
            return wellness;
        }
        public async Task<Wellness> CreateWellnessAsync(Wellness newWell)
        {
            //Look how you did during CC
            context.Wellnesses.Add(newWell);
            await context.SaveChangesAsync();

            return newWell;
        }
        public async Task<bool> UpdateWellnessAsync(Wellness well)
        {
            context.Wellnesses.Update(well);
            var result = await context.SaveChangesAsync();

            if (result > 0)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteWellnessAsync(int wellId)
        {
            var rowsAffected = await context.Wellnesses.Where(w => w.WellnessId == wellId).ExecuteDeleteAsync();

            if (rowsAffected > 0)
            {
                return true;
            }

            return false;
        }
    }
}
