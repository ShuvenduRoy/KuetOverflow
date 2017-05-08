using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using KuetOverflow.Data;
using KuetOverflow.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KuetOverflow.Controllers.Api
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/Notifications")]
    public class NotificationsController : Controller
    {
        private SchoolContext _context;

        public NotificationsController(SchoolContext context)
        {
            _context = context;
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