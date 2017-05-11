using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using KuetOverflow.Data;
using KuetOverflow.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KuetOverflow.Controllers
{
    [Authorize]
    public class TweetsController : Controller
    {
        private readonly SchoolContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public TweetsController(SchoolContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        // GET: Tweets
        public async Task<IActionResult> Index()
        {
            var userId = PrincipalExtensions.FindFirstValue(this.User, ClaimTypes.NameIdentifier);

            var tweets = await Queryable.Where<Tweet>(_context.Tweet, t => t.UserId == userId)
                .AsNoTracking()
                .ToListAsync();

            foreach (var tweet in tweets)
            {
                tweet.UserImage = "https://graph.facebook.com/" + _userManager.FindByIdAsync(userId).Result.FbProfile + "/?fields=picture&type=large";
                tweet.UserName = _userManager.FindByIdAsync(tweet.UserId).Result.UserName;
            }

            return View(tweets);
        }

        // GET: Tweets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tweet = await EntityFrameworkQueryableExtensions.SingleOrDefaultAsync<Tweet>(_context.Tweet, m => m.Id == id);
            if (tweet == null)
            {
                return NotFound();
            }

            return View(tweet);
        }

        // GET: Tweets/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tweets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Body")] Tweet tweet)
        {
            var userId = PrincipalExtensions.FindFirstValue(this.User, ClaimTypes.NameIdentifier);
            tweet.UserId = userId;
            tweet.DateTime = DateTime.Now;
            

            if (ModelState.IsValid)
            {
                _context.Add(tweet);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(tweet);
        }

        // GET: Tweets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tweet = await EntityFrameworkQueryableExtensions.SingleOrDefaultAsync<Tweet>(_context.Tweet, m => m.Id == id);
            if (tweet == null)
            {
                return NotFound();
            }
            return View(tweet);
        }

        // POST: Tweets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Body,UserId,DateTime")] Tweet tweet)
        {
            if (id != tweet.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tweet);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TweetExists(tweet.Id))
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
            return View(tweet);
        }

        // GET: Tweets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tweet = await EntityFrameworkQueryableExtensions.SingleOrDefaultAsync<Tweet>(_context.Tweet, m => m.Id == id);
            if (tweet == null)
            {
                return NotFound();
            }

            return View(tweet);
        }

        // POST: Tweets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tweet = await EntityFrameworkQueryableExtensions.SingleOrDefaultAsync<Tweet>(_context.Tweet, m => m.Id == id);
            _context.Tweet.Remove(tweet);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool TweetExists(int id)
        {
            return Queryable.Any<Tweet>(_context.Tweet, e => e.Id == id);
        }
    }
}
