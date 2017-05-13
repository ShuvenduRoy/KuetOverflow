namespace KuetOverflow.Models
{
    public class Follow
    {
        public int ID { get; set; }
        public int FollowerId { get; set; }
        public int FolloweeId { get; set; }

        public TwitterUser Follower { get; set; }
        public TwitterUser Followee { get; set; }
    }
}
