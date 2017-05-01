using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KuetOverflow.Data;
using KuetOverflow.Models;
using Microsoft.ApplicationInsights.Extensibility.Implementation;
using Microsoft.AspNetCore.Identity;

namespace KuetOverflow.Controllers
{
    public class CoursesController : Controller
    {
        private readonly SchoolContext _context;

        public CoursesController(SchoolContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> Index()
        {
            var schoolContext = _context.Courses.Include(c => c.Department)
                .Include(c => c.Department)
                .AsNoTracking();
            return View(await schoolContext.ToListAsync());
        }

        public async Task<IActionResult> StudentIndex()
        {
            var schoolContext = _context.Courses.Include(c => c.Department)
                .Include(c => c.Department)
                .Include(c => c.Questions)
                .AsNoTracking();

            var userid = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var username = this.User.FindFirstValue(ClaimTypes.Name);

            return View(await schoolContext.ToListAsync());
        }

        public async Task<IActionResult> CourseQuestions(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ViewData["course"] = id;
            TempData["course"] = id;

            var questions = _context.Question
                .Where(q => q.CourseID == id)
                .ToListAsync();

            return View(await questions);
        }


        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses
                .Include(c => c.Department)
                .SingleOrDefaultAsync(m => m.CourseID == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }


        public IActionResult Create()
        {
            PopulateDepartmentsDropDownList();
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CourseNo,Title,Credits,DepartmentID")] Course course)
        {
            if (ModelState.IsValid)
            {
                _context.Add(course);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            PopulateDepartmentsDropDownList(course.DepartmentID);
            return View(course);
        }


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses.SingleOrDefaultAsync(m => m.CourseID == id);
            if (course == null)
            {
                return NotFound();
            }
            PopulateDepartmentsDropDownList();
            return View(course);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CourseID,CourseNo,Title,Credits,DepartmentID")] Course course)
        {
            if (id != course.CourseID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(course);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseExists(course.CourseID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            ViewData["DepartmentID"] = new SelectList(_context.Departments, "DepartmentID", "DepartmentID", course.DepartmentID);
            return View(course);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses
                .Include(c => c.Department)
                .SingleOrDefaultAsync(m => m.CourseID == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var course = await _context.Courses.SingleOrDefaultAsync(m => m.CourseID == id);
            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool CourseExists(int id)
        {
            return _context.Courses.Any(e => e.CourseID == id);
        }

        private void PopulateDepartmentsDropDownList(object selectedDepartment = null)
        {
            var departmentQuery = from d in _context.Departments
                orderby d.Name
                select d;
            ViewBag.DepartmentID = new SelectList(departmentQuery.AsNoTracking(), "DepartmentID", "Name", selectedDepartment);
        }
    }
}
