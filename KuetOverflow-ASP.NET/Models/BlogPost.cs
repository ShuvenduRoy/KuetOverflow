using System;

namespace KuetOverflow_ASP.NET.Models
{
    public class BlogPost
    {
        public long Id { get; set; }
        public string Author { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DateTime { get; set; }
    }
}
