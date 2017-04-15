using System.Collections.Generic;

namespace KuetOverflow.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double CGPA { get; set; }

        public ICollection<Enrollment> Enrollments { get; set; }
    }
}
