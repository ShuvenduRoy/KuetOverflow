using System.Collections.Generic;

namespace KuetOverflow.Models.SchoolViewModels
{
    public class Lecture_LectureListViewModel
    {
        public int Course_ID { get; set; }
        public IEnumerable<Lecture> Lectures { get; set; }
        public LectureCommentViewModel LectureCommentViewModel { get; set; }
    }
}
