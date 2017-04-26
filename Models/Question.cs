using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KuetOverflow.Models
{
    public class Question
    {
        public int ID { get; set; }
        public int CourseID { get; set; }
        public string Title { get; set; }
        public string UserName { get; set; }
        public string UserId { get; set; }
        public DateTime DateTime { get; set; }
    }
}
