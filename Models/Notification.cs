using System;

namespace KuetOverflow.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Body { get; set; }
        public DateTime Time { get; set; }

        public string DateConverter(TimeSpan time)
        {
            if (time.Days > 0)
                return time.Days.ToString() + " days ago";
            else if (time.Hours > 0)
                return time.Hours.ToString() + " hours ago";
            else
                return time.Minutes.ToString() + " minutes ago";

        }
    }
}
