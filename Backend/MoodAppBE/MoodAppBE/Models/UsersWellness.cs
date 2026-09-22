using System.ComponentModel.DataAnnotations.Schema;

namespace MoodAppBE.Models
{
    public class UsersWellness
    {
        public int UsersWellnessId { get; set; }
        [ForeignKey("User")]
        public int Id { get; set; }
        public User User { get; set; }

        public int WellnessId { get; set; }
        public Wellness Wellness { get; set; }

        public DateTime CreationDate { get; set; }

    }
}
