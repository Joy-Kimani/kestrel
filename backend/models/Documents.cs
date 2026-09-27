using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public class Documents
    {
        public Guid Id{get; set;}
        public Guid ProjectId{get; set;}
        [ForeignKey(nameof(ProjectId))]
        public Project Projects{get; set;} = null!;

        public string Title{get; set;} = null!;
        public string? Content{get; set;}
        public Guid CreatedById{get; set;}
        [ForeignKey(nameof(CreatedById))]
        public User Users{get; set;} = null!;
        public DateTime CreatedAt{get; set;} = DateTime.UtcNow;
        public DateTime UpdatedAt{get; set;} = DateTime.UtcNow;
    }
}