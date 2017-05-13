using System.Collections.Generic;

namespace KuetOverflow.Models.SchoolViewModels
{
    public class TweetHomePageViewModel
    {
        public TwitterUser User { get; set; }
        public IEnumerable<Tweet> Tweets { get; set; }
    }
}
