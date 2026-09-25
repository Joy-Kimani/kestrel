using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public class Team
    {
        public Guid TeamId { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public Guid OwnerId { get; set; }

        [ForeignKey(nameof(OwnerId))]
        public User Owner {get; set;} = null!;
 
        public ICollection<User> Users { get; set; } = new List<User>();
        public ICollection<Project> Projects { get; set; } = new List<Project>();
    }
}