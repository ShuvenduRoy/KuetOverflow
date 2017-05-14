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
    public class QuestionsController : Controller
    {
        private readonly SchoolContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public QuestionsController(SchoolContext context , UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Questions
        public async Task<IActionResult> Index()
        {
            return View(await _context.Question.ToListAsync());
        }

        // GET: Questions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            TempData["que"] = id;

            var question = await _context.Question
                .SingleOrDefaultAsync(m => m.ID == id);
            QuestionViewModel qvm = new QuestionViewModel();
            qvm.Question = question;

            qvm.Question.UserImage = "https://graph.facebook.com/"+ _userManager.FindByIdAsync(question.UserId).Result.FbProfile+ "/?fields=picture&type=large";

            var UserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var votes = await _context.Votes
                .Where(v=> v.QuestionID == id)
                .ToListAsync();

            foreach (var vote in votes)
            {
                if (vote.UserID == UserId)
                {
                    question.Vote = vote.Value;
                }
            }

            var stars = await _context.Stars
                .Where(s => s.QuestionID == id)
                .ToListAsync();


            foreach (var star in stars)
            {
                if (star.UserID == UserId)
                    question.Star = true;
            }


            var answers = await _context.Answer
                .Where(a => a.QuestionID == id)
                .ToListAsync();
            qvm.Answers = answers;

            foreach (var answer in qvm.Answers)
            {
                answer.UserImage = "https://graph.facebook.com/" + _userManager.FindByIdAsync(answer.UserId).Result.FbProfile + "/?fields=picture&type=large";
                answer.UserName = _userManager.FindByIdAsync(answer.UserId).Result.UserName;
            }

            if (question == null)
            {
                return NotFound();
            }

            return View(qvm);
        }

        // GET: Questions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Questions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Title,Description ")] Question question)
        {
            int n = int.Parse(TempData["course"].ToString());
            question.CourseID = n;
            question.DateTime = DateTime.Now;
            question.UserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            question.UserName = User.Identity.Name;

            if (ModelState.IsValid)
            {
                _context.Add(question);
                await _context.SaveChangesAsync();
                return RedirectToAction("CourseQuestions", "Courses", new {id=question.CourseID});
            }
            return View(question);
        }

        // GET: Questions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var question = await _context.Question.SingleOrDefaultAsync(m => m.ID == id);
            if (question == null)
            {
                return NotFound();
            }
            return View(question);
        }

        // POST: Questions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,CourseID,Title,Description,UserName,UserId,DateTime")] Question question)
        {
            if (id != question.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(question);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuestionExists(question.ID))
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
            return View(question);
        }

        // GET: Questions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var question = await _context.Question
                .SingleOrDefaultAsync(m => m.ID == id);
            if (question == null)
            {
                return NotFound();
            }

            return View(question);
        }

        // POST: Questions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var question = await _context.Question.SingleOrDefaultAsync(m => m.ID == id);
            _context.Question.Remove(question);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool QuestionExists(int id)
        {
            return _context.Question.Any(e => e.ID == id);
        }

        public async Task<IActionResult> UpVote(int id)
        {
            var question = await _context.Question
                .SingleOrDefaultAsync(q => q.ID == id);


            var votes = await _context.Votes
                .Where( v=> v.QuestionID == id)
                .ToListAsync();

            var UserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);


            var stars = await _context.Stars
                .Where(s => s.QuestionID == id)
                .ToListAsync();


            foreach (var star in stars)
            {
                if (star.UserID == UserId)
                    question.Star = true;
            }


            Vote UserVote = new Vote();
            UserVote.UserID = UserId;
            UserVote.QuestionID = id;

            foreach (var vote in votes)
            {

                if (vote.UserID == UserId)
                {
                    question.Vote = vote.Value;
                    UserVote.ID = vote.ID;
                    UserVote.Value = vote.Value;
                }
            }

            if (UserVote.Value< 1)
            {
                question.Vote += 1;
                UserVote.Value +=1;
                question.TotalVote += 1;


                if (UserVote.ID != 0)
                {
                    var vote = await _context.Votes.SingleOrDefaultAsync(m => m.ID == UserVote.ID);
                    _context.Votes.Remove(vote);
                    await _context.SaveChangesAsync();

                    UserVote.ID = 0;
                }

                _context.Add(UserVote);await _context.SaveChangesAsync();
            }


            _context.Update(question);
            await _context.SaveChangesAsync();
            return  PartialView("_Vote", question);


        }

        public async Task<IActionResult> DownVote(int id)
        {
            var question = await _context.Question
                .SingleOrDefaultAsync(q => q.ID == id);


            var votes = await _context.Votes
                .Where(v => v.QuestionID == id)
                .ToListAsync();

            var UserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);


            var stars = await _context.Stars
                .Where(s => s.QuestionID == id)
                .ToListAsync();


            foreach (var star in stars)
            {
                if (star.UserID == UserId)
                    question.Star = true;
            }

            Vote UserVote = new Vote();
            UserVote.UserID = UserId;
            UserVote.QuestionID = id;

            foreach (var vote in votes)
            {
                if (vote.UserID == UserId)
                {
                    question.Vote = vote.Value;
                    UserVote.ID = vote.ID;
                    UserVote.Value = vote.Value;
                }
            }

            if (question.Vote >-1)
            {
                question.Vote -= 1;
                UserVote.Value = question.Vote;
                question.TotalVote -= 1;

                if (UserVote.ID != 0)
                {
                    var vote = await _context.Votes.SingleOrDefaultAsync(m => m.ID == UserVote.ID);
                    _context.Votes.Remove(vote);
                    await _context.SaveChangesAsync();

                    UserVote.ID = 0;
                }
               
                _context.Add(UserVote);
                
            }

            _context.Update(question);
            await _context.SaveChangesAsync();
            return PartialView("_Vote", question);


        }


        public async Task<IActionResult> StarClicked(int id)
        {
            var question = await _context.Question
                .SingleOrDefaultAsync(q => q.ID == id);


            var votes = await _context.Votes
                .Where(v => v.QuestionID == id)
                .ToListAsync();

            var UserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);


            var stars = await _context.Stars
                .Where(s => s.QuestionID == id)
                .ToListAsync();

            foreach (var vote in votes)
            {

                if (vote.UserID == UserId)
                {
                    question.Vote = vote.Value;
                }
            }


            int starid = -1;
            foreach (var star in stars)
            {
                if (star.UserID == UserId)
                {
                    question.Star = true;
                    starid = star.ID;
                }
                    
            }


            question.Star = !question.Star;

            if (starid > 0)
            {
                var remove_star = _context.Stars.SingleOrDefault(s => s.ID == starid);
                _context.Stars.Remove(remove_star);
                await _context.SaveChangesAsync();

                question.Star = false;
            }

            else
            {
                Star Newstar = new Star();
                Newstar.QuestionID = id;
                Newstar.UserID = UserId;

                _context.Stars.Add(Newstar);
                await _context.SaveChangesAsync();
                question.Star = true;
            }



            return PartialView("_Vote", question);


        }

        public async Task<IActionResult> PopularQuestion()
        {
            var question = await _context.Question
                .ToListAsync();

            return PartialView(question);
        }

    }
}
