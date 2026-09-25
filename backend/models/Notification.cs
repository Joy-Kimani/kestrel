namespace backend.Models
{
      public class Notification
    {
        public Guid NotificationId { get; set; }
        public Guid UserId { get; set; }
        public Guid? BugId { get; set; }
        public string Message { get; set; } = string.Empty;
        public bool IsRead { get; set; }
        public DateTime CreatedAt { get; set; }
 
        public User User { get; set; } = null!;

    }
}