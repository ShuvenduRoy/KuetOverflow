using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace KuetOverflow.Models
{
    public class Student
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string Name { get; set; }
        public double CGPA { get; set; }

        public ICollection<Enrollment> Enrollments { get; set; }
    }
}
