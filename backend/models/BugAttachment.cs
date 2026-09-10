namespace backend.Models{

    public class BugAttachment
    {
        public Guid AttachmentId { get; set; }
        public Guid BugId { get; set; }
        public Guid UploadedByUserId { get; set; }
        public string FileUrl { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
 
        public Bug Bug { get; set; } = null!;
        public User UploadedByUser { get; set; } = null!;
    }

}