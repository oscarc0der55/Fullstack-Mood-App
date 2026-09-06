namespace MoodAppBE.DTO.Mood
{
    public class MoodDTO
    {
        public int MoodId { get; set; }
        public string Status { get; set; }
        public string Activity { get; set; }
        public string SleepQuality { get; set; }

        public MoodDTO()
        {

        }
    }
}
