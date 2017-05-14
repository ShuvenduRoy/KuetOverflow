using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace KuetOverflow.Models
{
    public class Tweet
    {
        public int Id { get; set; }
        public string Body { get; set; }
        public string UserId { get; set; }
        public int TweetUserID { get; set; } = 0;
        public DateTime DateTime { get; set; }

        [NotMapped]
        public string UserName { get; set; }

        [NotMapped]
        public string UserImage { get; set; }

    }
}
