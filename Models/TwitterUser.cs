namespace KuetOverflow.Models
{
    public class TwitterUser
    {
        public int ID { get; set; }
        public string UserID { get; set; }
        public int Follower { get; set; }
        public int Followee { get; set; }
    }
}
