namespace MoodAppBE.Models
{
    public class Wellness
    {
        public int WellnessId { get; set; }
        public string Activity { get; set; }
        public string Food { get; set; }
        public string? SleepQuality { get; set; }

        public List<UsersMood> UsersMoods { get; set; }
    }
}
