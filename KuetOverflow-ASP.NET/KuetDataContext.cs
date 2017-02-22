using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KuetOverflow_ASP.NET.Models;
using Microsoft.EntityFrameworkCore;

namespace KuetOverflow_ASP.NET
{
    
    public class KuetDataContext: DbContext
    {
        public DbSet<BlogPost> Posts { get; set; }

        public KuetDataContext(DbContextOptions<KuetDataContext> options):base(options)
        {
            Database.EnsureCreated();
        }
    }
}
