namespace MoodAppBE.Models
{
    public class Wellness
    {
        public int WellnessId { get; set; }
        public string Activity { get; set; }
        public string Food { get; set; }
        public string? SleepQuality { get; set; }
        public DateTime CreationDate { get; set; }

        // Foreign key to User
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
