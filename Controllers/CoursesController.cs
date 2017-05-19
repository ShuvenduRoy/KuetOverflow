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
using KuetOverflow.Models.SchoolViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace KuetOverflow.Controllers
{
    [Authorize]
    public class CoursesController : Controller
    {
        private readonly SchoolContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public CoursesController(SchoolContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
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
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var student = _context.Students
                .Where(s => s.UserID == userId)
                .AsNoTracking()
                .SingleOrDefault();

            var model = new StudentIndexViewModel();


            var courseEnrollment = await _context.Enrollments
                .Where(e => e.StudentID == student.ID)
                .Include(e => e.Course)
                    .ThenInclude(c => c.Department)
                .Include(e => e.Course)
                    .ThenInclude(c => c.Questions)
                .AsNoTracking()
                .ToListAsync();

            model.Enrollments = courseEnrollment;

            try
            {
                var tweetUserId = _context.TwitterUsers
                    .SingleOrDefault(u => u.UserID == userId)
                    .ID;

                var following = _context.Follows
                    .Where(t => t.FollowerId == tweetUserId)
                    .Select(t => t.FolloweeId);

                model.Tweets = _context.Tweet
                    .OrderByDescending(x => x.DateTime)
                    .Where(u => following.Contains(u.TweetUserID))
                    .Take(10);

                foreach (var tweet in model.Tweets)
                {
                    var user = _userManager.FindByIdAsync(tweet.UserId).Result;

                    tweet.UserImage = "https://graph.facebook.com/" + user.FbProfile + "/?fields=picture&type=large";
                    tweet.UserName = user.UserName;
                }
            }
            catch (Exception e)
            {
                
            }
            


            try
            {
                return View(model);
            }
            catch (Exception e)
            {
                return View();
            }



        }

        public async Task<IActionResult> CourseQuestions(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            TempData["course"] = id;


            var model = new QuestionLectureViewModel();
            model.CourseId = (int) id;
            model.Questions = await _context.Question
                .Where(q => q.CourseID == id)
                .AsNoTracking()
                .ToListAsync();
            foreach (var question in model.Questions)
            {
                question.UserImage = "https://graph.facebook.com/" + _userManager.FindByIdAsync(question.UserId).Result.FbProfile + "/?fields=picture&type=large";
            }

            model.Lectures = await _context.Lecture
                .Where(l => l.CourseId == id)
                .AsNoTracking()
                .ToListAsync();


            return View(model);
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
