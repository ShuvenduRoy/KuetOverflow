using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KuetOverflow.Models.SchoolViewModels
{
    public class QuestionViewModel
    {
        public Question Question { get; set; }
        public IEnumerable<Answer> Answers { get; set; }
    }
}
