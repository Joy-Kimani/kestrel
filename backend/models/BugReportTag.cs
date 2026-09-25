using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public class BugReportTag
    {
        public Guid BugReportId{get; set;}
        [ForeignKey(nameof(BugReportId))]
        public BugReport BugReports{get; set;} = null!;

        public Guid TagId{get; set;}
        [ForeignKey(nameof(TagId))]
        public Tags Tag{get; set;} = null!;
        
    }
}