namespace MoodAppBE.Models
{
    public class UsersWellness
    {
        public int UsersWellnessId { get; set; }
        public int Id { get; set; }
        public User User { get; set; }

        public int WellnessId { get; set; }
        public Wellness Wellness;

        public DateTime CreationDate { get; set; }

    }
}
