using System;

namespace KuetOverflow.Controllers.Api
{
    public class NotificationDto
    {
        public string Body { get; set; }
        public DateTime Time { get; set; }

        public UserDto User { get; set; }
    }
}