using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    // [Index(nameof(Email), IsUnique = true)]
     public enum UserRole
    {
        QA,
        Engineer,
        Lead
    }
    public class User
    {
        public Guid UserId { get; set; }
        public Guid TeamId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(256)]
        public string Email { get; set; } = string.Empty;

        [Required]
        public UserRole Role { get; set; }// e.g. "QA", "Engineer", "Lead"
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string Password {get; set;} = string.Empty;
        public bool IsActive {get; set;}
        public DateTimeOffset LastActiveAt {get; set;}
        public string? AvatarUrl {get; set;}

        [ForeignKey(nameof(TeamId))]
        public Team Team { get; set; } = null!;
 
        // Split navigation collections since a user can both report and be assigned bugs
        // public ICollection<Bug> ReportedBugs { get; set; } = new List<Bug>();
        // public ICollection<Bug> AssignedBugs { get; set; } = new List<Bug>();
 
        public ICollection<BugComment> Comments { get; set; } = new List<BugComment>();
        public ICollection<Notification> Notifications { get; set; } = new List<Notification>();
    }
}