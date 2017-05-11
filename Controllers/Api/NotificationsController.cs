using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using KuetOverflow.Data;
using KuetOverflow.Dtos;
using KuetOverflow.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace KuetOverflow.Controllers.Api
{

    [Produces("application/json")]
    [Route("api/Notifications")]
    [Authorize]
    public class NotificationsController : Controller
    {
        private SchoolContext _context;
        private UserManager<ApplicationUser> _userManager;

        public NotificationsController(SchoolContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IEnumerable<NotificationDto> GetNotifications()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var notifications = _context.UserNotifications
                .Where(n => n.UserId == userId && n.IsRead==false)
                .Select(un => un.Notification)
                .ToList();

            return notifications.Select(n=>new NotificationDto()
            {
                Body = n.Body,
                Time = n.Time,
                TimeDiffrence = n.DateConverter(DateTime.Now - n.Time),
                User = new UserDto()
                {
                    Name = _userManager.FindByIdAsync(n.UserId).Result.UserName
                }
            });

        }

        [HttpPost]
        [Route("MarkAsRead")]
        public void MarkAsRead()
        {
            var UserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var notifications = _context.UserNotifications
                .Where(un => un.UserId == UserId && !un.IsRead)
                .ToList();

            notifications.ForEach(n => n.Read());
            _context.SaveChanges();

        }



    }
}