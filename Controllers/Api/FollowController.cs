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
        public void AddFollower(int id)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = _context.TwitterUsers
                .SingleOrDefault(u => u.UserID == userId);

            Follow follow = new Follow
            {
                FollowerId = id,
                FolloweeId = user.ID
            };

            _context.Add(follow);
            _context.SaveChangesAsync();
        }
    }
}