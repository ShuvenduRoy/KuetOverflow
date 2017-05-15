using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KuetOverflow.Models.SchoolViewModels
{
    public class ChatPageViewModel
    {
        public IEnumerable<Message> Messages { get; set; }
        public IEnumerable<TwitterUser> Users { get; set; }
    }
}
