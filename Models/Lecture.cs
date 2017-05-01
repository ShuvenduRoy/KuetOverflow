using System;

namespace KuetOverflow.Models
{
    public class Lecture
    {
        public int ID { get; set; }
        public int CourseId { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime UpdateTime { get; set; }
    }
}
