using System.Collections.Generic;

namespace KuetOverflow.Models.SchoolViewModels
{
    public class LectureCommentViewModel
    {
        public Lecture Lecture { get; set; }
        public IEnumerable<Comment> Comments { get; set; }
    }
}
