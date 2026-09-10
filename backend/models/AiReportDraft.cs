namespace backend.Models
{
    public class AiReportDraft
    {
        public Guid DraftId { get; set; }
        public Guid BugId { get; set; }
        public string RawInput { get; set; } = string.Empty;
        public string GeneratedOutput { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
 
        public Bug Bug { get; set; } = null!;
    }
}