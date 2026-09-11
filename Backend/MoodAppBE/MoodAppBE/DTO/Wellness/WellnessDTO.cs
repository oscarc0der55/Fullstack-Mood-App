namespace MoodAppBE.DTO.Wellness
{
    public class WellnessDTO
    {
        public int WellnessId { get; set; }
        public string Activity { get; set; }
        public string Food { get; set; }
        public string? SleepQuality { get; set; }

        public WellnessDTO()
        {

        }
    }
}
