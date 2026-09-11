namespace MoodAppBE.DTO.Mood
{
    public class MoodDTO
    {
        public int MoodId { get; set; }
        public string Status { get; set; }
        public string? Troubles { get; set; }

        public MoodDTO()
        {

        }
    }
}
