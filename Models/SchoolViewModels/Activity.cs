using System.Collections.Generic;

namespace KuetOverflow.Models.SchoolViewModels
{
    public class Activity
    {
        public IEnumerable<Question> Questions { get; set; }
        public IEnumerable<Answer> Answers { get; set; }
    }
}
