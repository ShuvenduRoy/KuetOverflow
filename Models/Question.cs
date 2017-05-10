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
        public int TotalVote { get; set; } = 0;
        public int TotalAnswers { get; set; }
        public int TotalStars { get; set; }
        public int Views { get; set; }

        [NotMapped]
        public bool Star { get; set; } = false;

        [NotMapped]
        public int Vote{ get; set; } = 0;

        [NotMapped]
        public string UserImage { get; set; } = "";

    }
}
