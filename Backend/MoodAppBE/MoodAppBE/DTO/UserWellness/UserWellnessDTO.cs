namespace MoodAppBE.DTO.UserWellness
{
    public class UserWellnessDTO
    {
        public int UsersWellnessId { get; set; }

        public int UserId { get; set; }
        public string? UserName { get; set; }

        public int WellnessId { get; set; }
        public string Activity { get; set; }
        public string Food { get; set; }
        public string? SleepQuality { get; set; }

        public DateTime CreationDate { get; set; }
    }
}