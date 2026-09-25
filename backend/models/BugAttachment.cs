using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models{

    public class BugAttachment
    {
        public Guid AttachmentId { get; set; }
        public Guid BugReportId { get; set; }
        [ForeignKey(nameof(BugReportId))]
        public BugReport BugReport {get; set;} = null!;
        public Guid UploadedByUserId { get; set; }
        [ForeignKey(nameof(UploadedByUserId))]
        public User UploadedByUser { get; set; } = null!;
        public string FileUrl { get; set; } = string.Empty;
        public string FileType {get; set;} = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }

}