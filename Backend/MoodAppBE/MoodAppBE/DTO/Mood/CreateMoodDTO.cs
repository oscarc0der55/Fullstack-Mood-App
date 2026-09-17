using System.ComponentModel.DataAnnotations;

namespace MoodAppBE.DTO.Mood
{
    public class CreateMoodDTO
    {
        [Required(ErrorMessage = "Status is required")]
        public int Status { get; set; }
        public string? Troubles { get; set; }
    }
}
