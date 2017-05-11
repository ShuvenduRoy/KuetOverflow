using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KuetOverflow.Data;
using KuetOverflow.Models;
using KuetOverflow.Models.SchoolViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace KuetOverflow.Controllers
{
    [Authorize]
    public class LecturesController : Controller
    {
        private readonly SchoolContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public LecturesController(SchoolContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager; 
        }


        public async Task<IActionResult> Index()
        {
            return View(await _context.Lecture.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id, int course_id)
        {
            if (id == null)
            {
                return NotFound();
            }

            TempData["lecture"] = id;
            TempData["cur_id"] = id;

            var model = new LectureCommentViewModel();

            model.Lecture = await _context.Lecture
                .SingleOrDefaultAsync(m => m.ID == id);
            model.Comments = await _context.Comment
                .Where(c => c.LectureID == id)
                .AsNoTracking()
                .ToListAsync();

            foreach (var comment in model.Comments)
            {
                comment.UserImage = "https://graph.facebook.com/" + _userManager.FindByIdAsync(comment.UserId).Result.FbProfile + "/?fields=picture&type=large";
                comment.UserName = _userManager.FindByIdAsync(comment.UserId).Result.UserName;
            }

            var viewModel = new Lecture_LectureListViewModel();
            viewModel.LectureCommentViewModel = model;
            viewModel.Course_ID = course_id;

            viewModel.Lectures = await _context.Lecture
                .Where(l => l.CourseId == course_id)
                .AsNoTracking()
                .ToListAsync();

            if (model.Lecture == null)
            {
                return NotFound();
            }

            return View(viewModel);
        }

        public async Task<IActionResult> Lecture(int? id, int course_id)
        {
            if (id == null)
            {
                return NotFound();
            }

            TempData["lecture"] = id;

            var model = new LectureCommentViewModel();

            model.Lecture = await _context.Lecture
                .SingleOrDefaultAsync(m => m.ID == id);
            model.Comments = await _context.Comment
                .Where(c => c.LectureID == id)
                .AsNoTracking()
                .ToListAsync();

            var viewModel = new Lecture_LectureListViewModel();
            viewModel.LectureCommentViewModel = model;
            viewModel.Course_ID = course_id;

            viewModel.Lectures = await _context.Lecture
                .Where(l => l.CourseId == course_id)
                .AsNoTracking()
                .ToListAsync();

            if (model.Lecture == null)
            {
                return NotFound();
            }

            return PartialView(viewModel);
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,CourseId,Title,Body")] Lecture lecture)
        {
            lecture.UpdateTime = DateTime.Now;

            var notification = new Notification();
            notification.Time = DateTime.Now;
            notification.UserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            notification.Body = " has added a new lecture: <i>" + lecture.Title + "</i>";

            var enrollments = _context.Enrollments
                .Where(e => e.CourseID == lecture.CourseId)
                .Select(s => s.Student.UserID)
                .AsNoTracking()
                .ToList();

            if (enrollments != null)
            {
                foreach (var enrollment in enrollments)
                {
                    var UserNotification = new UserNotification();
                    UserNotification.UserId = enrollment;
                    UserNotification.Notification = notification;

                    _context.UserNotifications.Add(UserNotification);
                }
            }
                

            if (ModelState.IsValid)
            {
                _context.Add(lecture);
                _context.Notifications.Add(notification);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(lecture);
        }

        // GET: Lectures/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lecture = await _context.Lecture.SingleOrDefaultAsync(m => m.ID == id);
            if (lecture == null)
            {
                return NotFound();
            }
            return View(lecture);
        }

        // POST: Lectures/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,CourseId,Title,Body")] Lecture lecture)
        {
            lecture.UpdateTime = DateTime.Now;

            if (id != lecture.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lecture);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LectureExists(lecture.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Details", new {id=id});
            }
            return View(lecture);
        }

        // GET: Lectures/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lecture = await _context.Lecture
                .SingleOrDefaultAsync(m => m.ID == id);
            if (lecture == null)
            {
                return NotFound();
            }

            return View(lecture);
        }

        // POST: Lectures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var lecture = await _context.Lecture.SingleOrDefaultAsync(m => m.ID == id);
            _context.Lecture.Remove(lecture);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool LectureExists(int id)
        {
            return _context.Lecture.Any(e => e.ID == id);
        }
    }
}
