namespace MoodAppBE.Models
{
    public class Mood
    {
        public int MoodId { get; set; }
        public string Status { get; set; }
        public string Activity { get; set; }
        public string SleepQuality { get; set; }
        public List<UsersMood> UsersMoods { get; set; }
    }
}
