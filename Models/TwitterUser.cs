using System.ComponentModel.DataAnnotations.Schema;

namespace KuetOverflow.Models
{
    public class TwitterUser
    {
        public int ID { get; set; }
        public string UserID { get; set; }
        public int Follower { get; set; }
        public int Followee { get; set; }

        [NotMapped]
        public string UserName { get; set; }

        [NotMapped]
        public string UserImage { get; set; }
    }
}
