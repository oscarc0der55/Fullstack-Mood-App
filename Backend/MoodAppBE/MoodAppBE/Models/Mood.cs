namespace MoodAppBE.Models
{
    public class Mood
    {
        public int MoodId { get; set; }
        public int Status { get; set; }
        public string? Troubles { get; set; }
        public DateTime CreationDate { get; set; }

        // Foreign key to User
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
