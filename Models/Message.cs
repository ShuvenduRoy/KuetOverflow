using System;

namespace KuetOverflow.Models
{
    public class Message
    {
        public int ID { get; set; }
        public int From { get; set; }
        public int To { get; set; }
        public string Body { get; set; }
        public DateTime DateTime { get; set; }
        public bool IsSeen { get; set; } = false;
    }
}
