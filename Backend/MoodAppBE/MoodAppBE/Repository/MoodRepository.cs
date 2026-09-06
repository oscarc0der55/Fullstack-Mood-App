using MoodAppBE.Data;
using MoodAppBE.Repository.IRepository;

namespace MoodAppBE.Repository
{
    public class MoodRepository : IMoodRepository
    {
        private readonly MoodAppDBContext context;

        public MoodRepository(MoodAppDBContext _context)
        {
            context = _context;
        }
    }
}
