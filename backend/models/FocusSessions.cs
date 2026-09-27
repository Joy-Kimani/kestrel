using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public class FocusSession
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public User User { get; set; } = null!;

        public Guid? BugReportId { get; set; }
        [ForeignKey(nameof(BugReportId))]
        public BugReport? BugReport { get; set; }

        public DateTime StartedAt { get; set; } = DateTime.UtcNow;
        public DateTime? EndedAt { get; set; }

        public int? DurationMinutes { get; set; }

        public string? Notes { get; set; }
    }
}