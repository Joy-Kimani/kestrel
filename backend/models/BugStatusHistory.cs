using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
        public class BugStatusHistory
    {
        public Guid Id { get; set; }
        public Guid BugReportId { get; set; }
        [ForeignKey(nameof(BugReportId))]
        public BugReport BugReport{get; set;} = null!;

        // public string FieldChanged { get; set; } = string.Empty;
        public string? OldStatus { get; set; }
        public string NewStatus { get; set; } = null!;
        public DateTime ChangedAt { get; set; } = DateTime.UtcNow;

        public Guid ChangedById { get; set; } 
        [ForeignKey(nameof(ChangedById))]
        public User UserId { get; set; } = null!;
        
    }
}