namespace MoodAppBE.Models
{
    public class UsersMood
    {
        public int UsersMoodId { get; set; }

        public int Id { get; set; }
        public User User { get; set; }

        public int MoodId { get; set; }
        public Mood Mood { get; set; }

        public DateTime CreationDate { get; set; }
    }
}
