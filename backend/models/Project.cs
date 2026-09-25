using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{

  public class Project
    {
        public Guid ProjectId { get; set; }
        public Guid TeamId { get; set; }
        [ForeignKey(nameof(TeamId))]
        public Team Team { get; set; } = null!;
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime CreatedAt {get; set;} = DateTime.UtcNow;
    }
}