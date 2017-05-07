using System.Collections.Generic;

namespace KuetOverflow.Models.SchoolViewModels
{
    public class QuestionViewModel
    {
        public Question Question { get; set; }
        public IEnumerable<Answer> Answers { get; set; }
    }
}
