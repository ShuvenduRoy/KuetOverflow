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
    public class ActivitiesController : Controller
    {
        private readonly SchoolContext _context;
        public UserManager<ApplicationUser> _UserManager { get; set; }

        public ActivitiesController(SchoolContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _UserManager = userManager;
        }

        // GET: Activities
        public async Task<IActionResult> Index()
        {
            var UserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var activity = new Activity();
            activity.UserID = UserId;
            activity.UserName = _UserManager.FindByIdAsync(activity.UserID).Result.UserName;
            activity.UserImage = "https://graph.facebook.com/" + _UserManager.FindByIdAsync(activity.UserID).Result.FbProfile + "/?fields=picture&type=large";

            activity.Questions = await _context.Question
                .Where(q => q.UserId == UserId)
                .AsNoTracking()
                .ToListAsync();

            activity.Answers = await _context.Answer
                .Where(a => a.UserId == UserId)
                .AsNoTracking()
                .ToListAsync();

            return View(activity);
        }

        // GET: Activities/Details/5
        public async Task<IActionResult> Details(string id)
        {

            var UserId = id;

            var activity = new Activity();
            activity.UserID = UserId;
            activity.UserName = _UserManager.FindByIdAsync(activity.UserID).Result.UserName;
            activity.UserImage = "https://graph.facebook.com/" + _UserManager.FindByIdAsync(activity.UserID).Result.FbProfile + "/?fields=picture&type=large";

            activity.Questions = await _context.Question
                .Where(q => q.UserId == UserId)
                .AsNoTracking()
                .ToListAsync();

            activity.Answers = await _context.Answer
                .Where(a => a.UserId == UserId)
                .AsNoTracking()
                .ToListAsync();

            return View(activity);
        }

        // GET: Activities/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Activities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID")] Activity activity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(activity);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(activity);
        }

        // GET: Activities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var activity = await _context.Activity.SingleOrDefaultAsync(m => m.ID == id);
            if (activity == null)
            {
                return NotFound();
            }
            return View(activity);
        }

        // POST: Activities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID")] Activity activity)
        {
            if (id != activity.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(activity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ActivityExists(activity.ID))
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
            return View(activity);
        }

        // GET: Activities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var activity = await _context.Activity
                .SingleOrDefaultAsync(m => m.ID == id);
            if (activity == null)
            {
                return NotFound();
            }

            return View(activity);
        }

        // POST: Activities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var activity = await _context.Activity.SingleOrDefaultAsync(m => m.ID == id);
            _context.Activity.Remove(activity);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool ActivityExists(int id)
        {
            return _context.Activity.Any(e => e.ID == id);
        }
    }
}
