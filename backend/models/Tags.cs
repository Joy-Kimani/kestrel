using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public class Tags
    {
        public Guid Id{get; set;}
        public Guid ProjectId {get; set;}
        [ForeignKey(nameof(ProjectId))]
        public Project Project{get; set;} = null!;
        public string Name{get; set;} = null!;
        public string? Colour{get; set;}
    }
}