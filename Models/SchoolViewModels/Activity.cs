using System.Collections.Generic;

namespace KuetOverflow.Models.SchoolViewModels
{
    public class Activity
    {
        public int ID { get; set; }
        public IEnumerable<Question> Questions { get; set; }
        public IEnumerable<Answer> Answers { get; set; }
    }
}
