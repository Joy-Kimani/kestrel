namespace backend.Models
{
    
    public enum BugSeverity { Low, Medium, High, Critical }
    public enum BugStatus { Open, InProgress, InReview, Resolved, Closed }
 
    public class Bug
    {
        public Guid BugId { get; set; }
        public Guid ProjectId { get; set; }
        public Guid ReportedByUserId { get; set; }
        public Guid? AssignedToUserId { get; set; }
 
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public BugSeverity Severity { get; set; }
        public int Priority { get; set; } // e.g. 1 (highest) - 5 (lowest), also mirrored in Redis sorted set
        public BugStatus Status { get; set; }
        public DateTime? DueAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
 
        public Project Project { get; set; } = null!;
        public User ReportedByUser { get; set; } = null!;
        public User? AssignedToUser { get; set; }
 
        public ICollection<BugComment> Comments { get; set; } = new List<BugComment>();
        public ICollection<BugAttachment> Attachments { get; set; } = new List<BugAttachment>();
        public ICollection<BugHistory> History { get; set; } = new List<BugHistory>();
        public ICollection<AiReportDraft> AiDrafts { get; set; } = new List<AiReportDraft>();
        public ICollection<Notification> Notifications { get; set; } = new List<Notification>();
    }
}