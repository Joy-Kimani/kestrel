using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public enum RoleTeam
    {
        Member, Lead, Admin
    }
    public class TeamMembers
    {
        public Guid TeamId {get; set;}
        [ForeignKey(nameof(TeamId))]
        public Team Team {get; set;} = null!;

        public Guid UserId {get; set;}
        [ForeignKey(nameof(UserId))]
        public User Users {get; set;} = null!;

        public RoleTeam RoleInTeam {get; set;}
        public DateTime JoinedAt {get; set;} = DateTime.UtcNow;
    }
}