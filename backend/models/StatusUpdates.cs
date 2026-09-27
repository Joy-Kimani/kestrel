using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public class StatusUpdates
    {
        public Guid Id{get; set;} = Guid.NewGuid();
        public Guid TeamId{get; set;}
        [ForeignKey(nameof(TeamId))]
        public Team Teams{get; set;} = null!;
        
        public Guid UserId{get; set;}
        [ForeignKey(nameof(UserId))]
        public User Users{get; set;} = null!;

        public string Content{get; set;} = null!;
        public DateTime CreatedAt{get; set;} = DateTime.UtcNow;

    }
}