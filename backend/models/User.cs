namespace backend.Models
{
    public class User
    {
        public Guid UserId { get; set; }
        public Guid TeamId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty; // e.g. "QA", "Engineer", "Lead"
        public DateTime CreatedAt { get; set; }
 
        public Team Team { get; set; } = null!;
 
        // Split navigation collections since a user can both report and be assigned bugs
        public ICollection<Bug> ReportedBugs { get; set; } = new List<Bug>();
        public ICollection<Bug> AssignedBugs { get; set; } = new List<Bug>();
 
        public ICollection<BugComment> Comments { get; set; } = new List<BugComment>();
        public ICollection<Notification> Notifications { get; set; } = new List<Notification>();
    }
}