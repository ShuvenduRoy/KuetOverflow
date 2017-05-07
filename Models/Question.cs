using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace KuetOverflow.Models
{
    public class Question
    {
        public int ID { get; set; }
        public int CourseID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string UserName { get; set; }
        public string UserId { get; set; }
        public DateTime DateTime { get; set; }

        [NotMapped]
        public bool Star { get; set; } = true;

        [NotMapped]
        public bool UpVote { get; set; } = true;

        [NotMapped]
        public bool DownVote { get; set; } = true;
    }
}
