using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace KuetOverflow.Models.SchoolViewModels
{
    public class TweetHomePageViewModel
    {
        public TwitterUser User { get; set; }
        public IEnumerable<Tweet> Tweets { get; set; }

        [NotMapped]
        public bool IsFollowing { get; set; } = false;
    }
}
