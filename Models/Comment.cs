using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace KuetOverflow.Models
{
    public class Comment
    {
        public int ID { get; set; }
        public int LectureID { get; set; }
        public string Body { get; set; }
        public string UserName { get; set; }
        public string UserId { get; set; }
        public DateTime DateTime { get; set; }

        [NotMapped]
        public string UserImage { get; set; }
    }
}
