using System;
using System.Collections.Generic;

namespace KuetOverflow.Models
{
    public class Vote
    {
        public int ID { get; set; }
        public int QuestionID { get; set; }
        public string UserID { get; set; }
        public int Value { get; set; }
    }
}
