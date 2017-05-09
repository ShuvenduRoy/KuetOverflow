using System;

namespace KuetOverflow.Dtos
{
    public class NotificationDto
    {
        public string Body { get; set; }
        public DateTime Time { get; set; }
        public String TimeDiffrence { get; set; }
        public UserDto User { get; set; }
    }
}