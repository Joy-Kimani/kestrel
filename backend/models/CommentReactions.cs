using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public class CommentReaction
    {
        public Guid Id {get; set;} = Guid.NewGuid();
        public Guid CommentId{get; set;}
        public Guid? UserId{get; set;}
        public string ReactionType{get; set;} = null!;
        public DateTime? CreatedAt{get; set;} = DateTime.UtcNow;
    }
}