using System.ComponentModel.DataAnnotations;

namespace MoodAppBE.DTO.Mood
{
    public class UpdateMoodDTO
    {
        public int Status { get; set; }
        public string? Troubles { get; set; }
    }
}
