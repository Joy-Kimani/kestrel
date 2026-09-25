using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public class BugReport
    {
        public Guid Id { get; set; }
        public Guid ProjectId { get; set; }
        public Project Project { get; set; } = null!;
        public Guid ReporterId { get; set; }
        [ForeignKey(nameof(ReporterId))]
        public User Reporter { get; set; } = null!;
        public Guid? AssigneeId { get; set; }
        [ForeignKey(nameof(AssigneeId))]
        public User? Assignee { get; set; }
        
        public string Title { get; set; } = string.Empty;
        public string? RawDescription { get; set; }
        public string? StructuredReport { get; set; }
        public string Status { get; set; } = "New";
        public string Priority { get; set; } = "Medium";
        public string? Severity { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? ResolvedAt { get; set; }
    }
}