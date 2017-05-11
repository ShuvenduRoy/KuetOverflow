using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace KuetOverflow.Models.SchoolViewModels
{
    public class Activity
    {
        public int ID { get; set; }
        public IEnumerable<Question> Questions { get; set; }
        public IEnumerable<Answer> Answers { get; set; }

        [NotMapped]
        public string UserID { get; set; }

        [NotMapped]
        public string UserImage { get; set; }

        [NotMapped]
        public string UserName { get; set; }

    }
}
