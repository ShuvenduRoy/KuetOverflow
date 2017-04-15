using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KuetOverflow.Models;

namespace KuetOverflow.Data
{
    public class DbInitializer
    {
        public static void Initialize(SchoolContext context)
        {
            context.Database.EnsureCreated();

            if (context.Students.Any())
            {
                return;
            }

            var students = new Student[]
            {
            new Student{Id=1407001,Name="Shuvendu Roy",CGPA=3.5},
            new Student{Id=1407002,Name="Tushar",CGPA=3.6},
            new Student{Id=1407003,Name="Hasib Iqbal",CGPA=3.28},
            new Student{Id=1407004,Name="Touhid jwarder",CGPA=3.68},
            new Student{Id=1407005,Name="Mehedi Hasan",CGPA=3.7},
            new Student{Id=1407006,Name="Zarin",CGPA=3.7},
            new Student{Id=1407007,Name="Asif",CGPA=3.00},
            new Student{Id=1407007,Name="Shoto",CGPA=3.6},
            new Student{Id=1407009,Name="Santo",CGPA=3.6}
            };
            foreach (Student s in students)
            {
                context.Students.Add(s);
            }
            context.SaveChanges();

            var courses = new Course[]
            {
            new Course{CourseID=1050,Title="Chemistry",Credits=3},
            new Course{CourseID=4022,Title="Database",Credits=3},
            new Course{CourseID=4041,Title="Web Programming",Credits=3},
            new Course{CourseID=1045,Title="Calculus",Credits=4},
            new Course{CourseID=3141,Title="Adbance Programming",Credits=4},
            new Course{CourseID=4021,Title="AI",Credits=3},
            new Course{CourseID=2042,Title="Machine Learning",Credits=4}
            };
            foreach (Course c in courses)
            {
                context.Courses.Add(c);
            }
            context.SaveChanges();

            var enrollments = new Enrollment[]
            {
            new Enrollment{StudentID=1,CourseID=1050},
            new Enrollment{StudentID=1,CourseID=4022},
            new Enrollment{StudentID=1,CourseID=4041},
            new Enrollment{StudentID=2,CourseID=1045},
            new Enrollment{StudentID=2,CourseID=3141},
            new Enrollment{StudentID=2,CourseID=2021},
            new Enrollment{StudentID=3,CourseID=1050},
            new Enrollment{StudentID=4,CourseID=1050},
            new Enrollment{StudentID=4,CourseID=4022},
            new Enrollment{StudentID=5,CourseID=4041},
            new Enrollment{StudentID=6,CourseID=1045},
            new Enrollment{StudentID=7,CourseID=3141},
            };
            foreach (Enrollment e in enrollments)
            {
                context.Enrollments.Add(e);
            }
            context.SaveChanges();
        }
    }
}
