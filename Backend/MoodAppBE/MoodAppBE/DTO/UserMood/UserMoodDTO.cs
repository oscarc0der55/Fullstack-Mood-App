namespace MoodAppBE.DTO.UserMood
{
    public class UserMoodDTO
    {
        public int UsersMoodId { get; set; }
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public int MoodId { get; set; }
        public int Status { get; set; }
        public string? Troubles { get; set; }
        public DateTime CreationDate { get; set; }
    }
}