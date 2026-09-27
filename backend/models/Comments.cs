using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public class Comment
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid? DocumentId { get; set; }
        [ForeignKey(nameof(DocumentId))]
        public Documents? Document { get; set; }

        public Guid? BugReportId { get; set; }
        [ForeignKey(nameof(BugReportId))]
        public BugReport? BugReport { get; set; }

        public Guid? ParentCommentId { get; set; }
        [ForeignKey(nameof(ParentCommentId))]
        public Comment? ParentComment { get; set; }
        public ICollection<Comment> Replies { get; set; } = new List<Comment>();

        public Guid AuthorId { get; set; }
        [ForeignKey(nameof(AuthorId))]
        public User Author { get; set; } = null!;

        public string Content { get; set; } = null!;
        public bool IsResolved { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}