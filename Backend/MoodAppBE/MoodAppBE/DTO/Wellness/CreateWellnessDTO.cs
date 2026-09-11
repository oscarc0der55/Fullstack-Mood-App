using System.ComponentModel.DataAnnotations;

namespace MoodAppBE.DTO.Wellness
{
    public class CreateWellnessDTO
    {
        [Required(ErrorMessage = "Activity is required")]
        public string Activity { get; set; }
        [Required(ErrorMessage = "Food is required")]
        public string Food { get; set; }
        public string? SleepQuality { get; set; }
    }
}
