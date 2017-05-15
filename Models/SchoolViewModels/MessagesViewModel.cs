using System.Collections.Generic;

namespace KuetOverflow.Models.SchoolViewModels
{
    public class MessagesViewModel
    {
        public int Id { get; set; }
        public IEnumerable<Message> Messages { get; set; }
    }
}
