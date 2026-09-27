using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
      public class Notification
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public User Users { get; set; } = null!;
        public string Type { get; set; } = null!;
        public string? PayloadJson {get; set;}
        public bool IsRead { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}