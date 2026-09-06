using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace MoodAppBE.Models
{
    public class User : IdentityUser<int>
    {
        public string? Name { get; set; }
        public List<UsersMood> UsersMoods { get; set; }
    }
}
