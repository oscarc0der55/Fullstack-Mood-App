namespace MoodAppBE.Models
{
    public class Mood
    {
        public int MoodId { get; set; }
        public int Status { get; set; }
        public string? Troubles { get; set; }
        public List<UsersMood> UsersMoods { get; set; }
    }
}
