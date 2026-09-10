namespace backend.Models
{
        public class BugHistory
    {
        public Guid HistoryId { get; set; }
        public Guid BugId { get; set; }
        public Guid ChangedByUserId { get; set; }
        public string FieldChanged { get; set; } = string.Empty;
        public string? OldValue { get; set; }
        public string? NewValue { get; set; }
        public DateTime ChangedAt { get; set; }
 
        public Bug Bug { get; set; } = null!;
        public User ChangedByUser { get; set; } = null!;
    }
}