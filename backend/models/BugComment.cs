namespace backend.Models{

    public class BugComment
    {
        public Guid CommentId { get; set; }
        public Guid BugId { get; set; }
        public Guid UserId { get; set; }
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
 
        public Bug Bug { get; set; } = null!;
        public User User { get; set; } = null!;
    }
}