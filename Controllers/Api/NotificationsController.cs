using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using KuetOverflow.Data;
using KuetOverflow.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KuetOverflow.Controllers.Api
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/Notifications")]
    public class NotificationsController : Controller
    {
        private SchoolContext _context;
        private UserManager<ApplicationUser> _userManager;

        public NotificationsController(SchoolContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IEnumerable<Notification> GetNotifications()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var notifications = _context.UserNotifications
                .Where(n => n.UserId == userId)
                .Select(un => un.Notification)
                .ToList();

            return notifications;
           
        }
    }
}