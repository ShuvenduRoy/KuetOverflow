using System.Linq;
using System.Security.Claims;
using KuetOverflow.Data;
using KuetOverflow.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace KuetOverflow.Controllers.Api
{
    [Produces("application/json")]
    
    public class FollowController : Controller
    {

        private SchoolContext _context;
        private UserManager<ApplicationUser> _userManager;

        public FollowController(SchoolContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpPost]
        public int AddFollower(int id)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = _context.TwitterUsers
                .SingleOrDefault(u => u.UserID == userId);

            var followUser = _context.TwitterUsers
                .SingleOrDefault(u => u.ID == id);

            var followHistory = _context.Follows
                .SingleOrDefault(f => (f.FollowerId == user.ID && f.FolloweeId == id));

            if (followHistory == null)
            {
                Follow follow = new Follow
                {
                    FollowerId = user.ID,
                    FolloweeId = id
                };

                _context.Add(follow);
                followUser.Follower += 1;

                _context.Update(followUser);
                _context.SaveChanges();

                return 1;
            }

            else
            {
                _context.Remove(followHistory);
                followUser.Follower -= 1;

                _context.Update(followUser);
                _context.SaveChanges();

                return -1;
            }

        }
    }
}