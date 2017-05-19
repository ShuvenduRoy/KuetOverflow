using System;
using System.Collections;
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
    public class MessagesController : Controller
    {
        private readonly SchoolContext _context;
        private readonly UserManager<ApplicationUser> _userManager;



        public MessagesController(SchoolContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;

        }

        // GET: Messages
        public async Task<IActionResult> Index()
        {
            return View(await _context.Messages.ToListAsync());
        }

        public async Task<IActionResult> GetAllMessages(int id=1)
        {
            var userid = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var tweetId = this._context.TwitterUsers
                .SingleOrDefault(t => t.UserID == userid)
                .ID;


            var messages =await _context.Messages
                .Where(m => (m.From == tweetId && m.To == id) || (m.From == id && m.To == tweetId))
                .ToListAsync();

            foreach (var message in messages)
            {
                if (message.To == tweetId)
                {
                    message.IsSeen = true;
                    _context.Update(message);
                }
            }

            await _context.SaveChangesAsync();

            var model = new MessagesViewModel();
            model.Id = id;
            model.Messages = messages;

            return PartialView("GetAllMessages",model);
        }

        public async Task<IActionResult> GetAllUnreadMessages(int id = 1)
        {
            var userid = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var tweetId = this._context.TwitterUsers
                .SingleOrDefault(t => t.UserID == userid)
                .ID;


            var messages = await _context.Messages
                .Where(m => ((m.From == id && m.To == tweetId)) && m.IsSeen == false)
                .ToListAsync();

            if (messages.Count > 0)
            {
                foreach (var message in messages)
                {
                    if (message.To == tweetId)
                    {
                        message.IsSeen = true;
                        _context.Update(message);
                    }
                }

                await _context.SaveChangesAsync();
            }


            var model = new MessagesViewModel();
            model.Id = id;
            model.Messages = messages;

            return PartialView("GetAllUnReadMessages", model);
        }


        public async Task<IActionResult> UserMessages()
        {
            var userid = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var tweetId = this._context.TwitterUsers
                .SingleOrDefault(t => t.UserID == userid)
                .ID;

            var messages = await _context.Messages
                .Where(m => m.From == tweetId || m.To == tweetId)
                .ToListAsync();

            List<TwitterUser> users = new List<TwitterUser>();
            var hash = new HashSet<int>();

            foreach (var message in messages)
            {

                if (message.To == tweetId)
                {
                    hash.Add(message.From);
                }
                else
                {
                    hash.Add(message.To);
                }
            }

            foreach (var i in hash)
            {
                TwitterUser user = new TwitterUser(i, _userManager, _context);
                users.Add(user);
            }

            return View(users);
        }



        // GET: Messages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var message = await _context.Messages
                .SingleOrDefaultAsync(m => m.ID == id);
            if (message == null)
            {
                return NotFound();
            }

            return View(message);
        }

        // GET: Messages/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Messages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,From,To,Body,DateTime,IsSeen")] Message message)
        {
            if (ModelState.IsValid)
            {
                _context.Add(message);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(message);
        }

        [HttpPost]
        public async Task<IActionResult> SentMessage(Message message)
        {
            var UserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            message.From = _context.TwitterUsers
                .SingleOrDefault(t => t.UserID == UserId)
                .ID;
            message.DateTime = DateTime.Now;

            if (ModelState.IsValid)
            {
                _context.Add(message);
                await _context.SaveChangesAsync();
            }

            return PartialView("GetJustSentMessages", message);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateByUser([Bind("ID,Body")] Message message, int user)
        {
            var userid = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var tweetId = this._context.TwitterUsers
                .SingleOrDefault(t => t.UserID == userid)
                .ID;

            message.From = tweetId;
            message.To = user;
            message.DateTime = DateTime.Now;

            if (ModelState.IsValid)
            {
                _context.Add(message);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        // GET: Messages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var message = await _context.Messages.SingleOrDefaultAsync(m => m.ID == id);
            if (message == null)
            {
                return NotFound();
            }
            return View(message);
        }

        // POST: Messages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,From,To,DateTime,IsSeen")] Message message)
        {
            if (id != message.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(message);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MessageExists(message.ID))
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
            return View(message);
        }

        // GET: Messages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var message = await _context.Messages
                .SingleOrDefaultAsync(m => m.ID == id);
            if (message == null)
            {
                return NotFound();
            }

            return View(message);
        }

        // POST: Messages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var message = await _context.Messages.SingleOrDefaultAsync(m => m.ID == id);
            _context.Messages.Remove(message);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool MessageExists(int id)
        {
            return _context.Messages.Any(e => e.ID == id);
        }

    }
}
