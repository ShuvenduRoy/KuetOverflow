using System.Collections.Generic;

namespace KuetOverflow.Models.SchoolViewModels
{
    public class QuestionLectureViewModel
    {
        public int CourseId { get; set; }
        public IEnumerable<Question> Questions { get; set; }
        public IEnumerable<Lecture> Lectures{ get; set; }
    }
}
