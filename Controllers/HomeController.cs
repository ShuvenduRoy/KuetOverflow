using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KuetOverflow.Data;
using KuetOverflow.Models.SchoolViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KuetOverflow.Controllers
{
    public class HomeController : Controller
    {
        private readonly SchoolContext _context;

        public HomeController(SchoolContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Route("api/home/about")]
        [HttpGet("[action]")]
        public IEnumerable<EnrollmentDateGroup> AboutData()
        {
            IQueryable<EnrollmentDateGroup> data = from student in _context.Students
                                                   group student by student.EnrollmentDate
                                                   into dataGroup
                                                   select new EnrollmentDateGroup()
                                                   {
                                                       EnrollmentDate = dataGroup.Key,
                                                       StudentCount = dataGroup.Count()
                                                   };
            return data;
        }

        public async Task<ActionResult> About()
        {
            IQueryable<EnrollmentDateGroup> data = from student in _context.Students
                group student by student.EnrollmentDate
                into dataGroup
                select new EnrollmentDateGroup()
                {
                    EnrollmentDate = dataGroup.Key,
                    StudentCount = dataGroup.Count()
                };
            return View(await data.AsNoTracking().ToListAsync());
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
