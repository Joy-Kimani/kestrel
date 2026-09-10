namespace backend.Models
{

  public class Project
    {
        public Guid ProjectId { get; set; }
        public Guid TeamId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
 
        public Team Team { get; set; } = null!;
        public ICollection<Bug> Bugs { get; set; } = new List<Bug>();
    }
}