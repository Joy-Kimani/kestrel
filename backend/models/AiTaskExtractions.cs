using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public class AiTaskExtractions
    {
        public Guid Id{get; set;}
        public Guid BugReportId{get; set;}
        [ForeignKey(nameof(BugReportId))]
        public BugReport BugReports{get; set;} = null!;
        public string RawInput{get; set;} = null!;
        public string ExtractedJson {get; set;} = null!;
        public string ModelUsed{get; set;} = null!;
        public DateTime CreatedAt{get; set;} = DateTime.UtcNow;
    }
}