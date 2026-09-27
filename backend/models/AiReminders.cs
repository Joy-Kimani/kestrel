using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public class AiReminders
    {
        public Guid Id{get; set;}
        public Guid UserId{get; set;}
        [ForeignKey(nameof(UserId))]
        public User Users{get; set;} = null!;

        public Guid BugReportId{get; set;}
        [ForeignKey(nameof(BugReportId))]
        public BugReport? BugReports{get; set;}

        public string ReminderText{get; set;} = null!;
        public DateTime TriggerAt{get; set;} 
        public bool IsSent{get; set;} = false;
        public DateTime CreatedAt{get; set;} = DateTime.UtcNow;
    }
}