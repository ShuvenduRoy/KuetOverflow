using System;
using System.Linq;
using System.Security.Claims;
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
            var model = new TweetHomePageViewModel();

            var userId = PrincipalExtensions.FindFirstValue(this.User, ClaimTypes.NameIdentifier);

            var tweets = await Queryable.Where<Tweet>(_context.Tweet, t => t.UserId == userId)
                .AsNoTracking()
                .ToListAsync();

            var twitterUser = _context.TwitterUsers
                .SingleOrDefault(t => t.UserID == userId);

            if (twitterUser == null)
                return RedirectToAction("Join",new {id=userId});

            model.User = twitterUser;
            model.User.UserImage = "https://graph.facebook.com/" + _userManager.FindByIdAsync(userId).Result.FbProfile + "/?fields=picture&type=large";
            model.User.UserName = _userManager.FindByIdAsync(twitterUser.UserID).Result.UserName;


            foreach (var tweet in tweets)
            {
                tweet.UserImage = "https://graph.facebook.com/" + _userManager.FindByIdAsync(userId).Result.FbProfile + "/?fields=picture&type=large";
                tweet.UserName = _userManager.FindByIdAsync(tweet.UserId).Result.UserName;
            }
            model.Tweets = tweets;

            return View(model);
        }

        public async Task<IActionResult> TweetUser(int id)
        {
            var user = _context.TwitterUsers
                .SingleOrDefault(p => p.ID == id);

            var userId = user.UserID;

            var model = new TweetHomePageViewModel();

            var tweets = await Queryable.Where<Tweet>(_context.Tweet, t => t.UserId == userId)
                .AsNoTracking()
                .ToListAsync();

            var twitterUser = _context.TwitterUsers
                .SingleOrDefault(t => t.UserID == userId);

            if (twitterUser == null)
                return RedirectToAction("Join", new { id = userId });

            model.User = twitterUser;
            model.User.UserImage = "https://graph.facebook.com/" + _userManager.FindByIdAsync(userId).Result.FbProfile + "/?fields=picture&type=large";
            model.User.UserName = _userManager.FindByIdAsync(twitterUser.UserID).Result.UserName;


            foreach (var tweet in tweets)
            {
                tweet.UserImage = "https://graph.facebook.com/" + _userManager.FindByIdAsync(userId).Result.FbProfile + "/?fields=picture&type=large";
                tweet.UserName = _userManager.FindByIdAsync(tweet.UserId).Result.UserName;
            }
            model.Tweets = tweets;

            var Follow = _context.Follows
                .SingleOrDefault(f => (f.FollowerId == twitterUser.ID && f.FolloweeId == id));

            if (Follow != null)
                model.IsFollowing = true;

            return View(model);
        }

        //public async Task<IActionResult> Join(string id)
        //{
        //    var model = new TwitterUser();
        //    model.UserID = id;
        //    return View(model);
        //}

        public async Task<IActionResult> Join(string id)
        {
            TwitterUser user = new TwitterUser();
            user.UserID = id;
            user.Followee = user.Follower = 0;
            _context.Add(user);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
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
            tweet.TweetUserID = _context.TwitterUsers.SingleOrDefault(u => u.UserID == userId).ID;
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

        public async Task<IActionResult> WhoToFollow()
        {
            var users = await _context.TwitterUsers.ToListAsync();

            foreach (var user in users)
            {
                user.UserName = _userManager.FindByIdAsync(user.UserID).Result.UserName;
                user.UserImage = "https://graph.facebook.com/" + _userManager.FindByIdAsync(user.UserID).Result.FbProfile + "/?fields=picture&type=large";
            }

            return PartialView(users);
        }
    }
}
