using System;

namespace KuetOverflow.Models
{
    public class Answer
    {
        public int ID { get; set; }
        public int QuestionID { get; set; }
        public string Title { get; set; }
        public string UserName { get; set; }
        public string UserId { get; set; }
        public DateTime DateTime { get; set; }


    }
}
