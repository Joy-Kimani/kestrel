using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public class UserSettings
    {
        public Guid UserId {get; set;}
        [ForeignKey(nameof(UserId))]
        public User Users{get; set;} = null!;
        public string Theme {get; set;} = "System";
        public string NotificationLevel {get; set;} = "All";
        public string? ApiKeysEncrypted {get; set;} 
        public DateTime UpdatedAt {get; set;} = DateTime.UtcNow;
    }
}