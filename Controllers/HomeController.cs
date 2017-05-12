using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using KuetOverflow.Data;
using KuetOverflow.Models;
using KuetOverflow.Models.SchoolViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KuetOverflow.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly SchoolContext _context;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public HomeController(SchoolContext context, SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            _signInManager = signInManager;
        }

        public async Task<IActionResult> Index()
        {
            var email = Request.Cookies["email"];//"bikash11roy@gmail.com";//
            var pass = Request.Cookies["password"];//"SR42@bikash";//

            if (!String.IsNullOrEmpty(email) && !String.IsNullOrEmpty(pass))
            {
                await _signInManager.PasswordSignInAsync(email, pass, false, lockoutOnFailure: false);

            }

            return Redirect("/public/index.html");
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
