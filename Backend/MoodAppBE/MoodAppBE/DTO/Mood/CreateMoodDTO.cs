using System.ComponentModel.DataAnnotations;

namespace MoodAppBE.DTO.Mood
{
    public class CreateMoodDTO
    {
        [Required(ErrorMessage = "Status is required")]
        public string Status { get; set; }
        [Required(ErrorMessage = "Activity is required")]
        public string Activity { get; set; }
        [Required(ErrorMessage = "SleepQuality is required")]
        public string SleepQuality { get; set; }
    }
}
