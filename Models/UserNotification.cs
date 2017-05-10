using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KuetOverflow.Models
{
    public class UserNotification
    {
        [Key]
        [Column(Order = 1)]
        public string UserId { get; set; }


        [Key]
        [Column(Order = 2)]
        public int NotificationId { get; set; }

        public bool IsRead { get; private set; }

        public Notification Notification { get; set; }

        public void Read()
        {
            this.IsRead = true;
        }
    }
}