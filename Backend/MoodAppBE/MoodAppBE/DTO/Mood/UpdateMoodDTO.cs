using System.ComponentModel.DataAnnotations;

namespace MoodAppBE.DTO.Mood
{
    public class UpdateMoodDTO
    {
        public string Status { get; set; }
        public string? Troubles { get; set; }
    }
}
