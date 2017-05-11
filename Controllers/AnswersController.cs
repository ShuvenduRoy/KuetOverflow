using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KuetOverflow.Data;
using KuetOverflow.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace KuetOverflow.Controllers
{
    [Authorize]
    public class AnswersController : Controller
    {
        private readonly SchoolContext _context;

        public AnswersController(SchoolContext context)
        {
            _context = context;    
        }

        // GET: Answers
        public async Task<IActionResult> Index()
        {
            return View(await _context.Answer.ToListAsync());
        }

        // GET: Answers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var answer = await _context.Answer
                .SingleOrDefaultAsync(m => m.ID == id);
            if (answer == null)
            {
                return NotFound();
            }

            return View(answer);
        }

        // GET: Answers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Answers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,QuestionID,Title")] Answer answer)
        {
            answer.DateTime = DateTime.Now;
            answer.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            answer.UserName = User.Identity.Name;
            if (ModelState.IsValid)
            {
                _context.Add(answer);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(answer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateByUser([Bind("ID,Title")] Answer answer)
        {
            answer.QuestionID = int.Parse(TempData["que"].ToString());
            answer.DateTime = DateTime.Now;
            answer.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            answer.UserName = User.Identity.Name;
            if (ModelState.IsValid)
            {
                _context.Add(answer);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Questions", new { id = answer.QuestionID });
                
            }
            return RedirectToAction("Index");
        }

        // GET: Answers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var answer = await _context.Answer.SingleOrDefaultAsync(m => m.ID == id);
            if (answer == null)
            {
                return NotFound();
            }
            return View(answer);
        }

        // POST: Answers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,QuestionID,Title,UserName,UserId,DateTime")] Answer answer)
        {
            if (id != answer.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(answer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnswerExists(answer.ID))
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
            return View(answer);
        }

        // GET: Answers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var answer = await _context.Answer
                .SingleOrDefaultAsync(m => m.ID == id);
            if (answer == null)
            {
                return NotFound();
            }

            return View(answer);
        }

        // POST: Answers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var answer = await _context.Answer.SingleOrDefaultAsync(m => m.ID == id);
            _context.Answer.Remove(answer);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool AnswerExists(int id)
        {
            return _context.Answer.Any(e => e.ID == id);
        }
    }
}
