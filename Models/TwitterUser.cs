using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using KuetOverflow.Data;
using Microsoft.AspNetCore.Identity;

namespace KuetOverflow.Models
{
    public class TwitterUser
    {
        public int ID { get; set; }
        public string UserID { get; set; }
        public int Follower { get; set; }
        public int Followee { get; set; }

        [NotMapped]
        public string UserName { get; set; }

        [NotMapped]
        public string UserImage { get; set; }

        public TwitterUser()
        {
            
        }

        public TwitterUser(int id, UserManager<ApplicationUser> _userManager, SchoolContext _context)
        {
            ID = id;
            string userId = _context.TwitterUsers.SingleOrDefault(u => u.ID == id).UserID;

            UserImage = "https://graph.facebook.com/" + _userManager.FindByIdAsync(userId).Result.FbProfile + "/?fields=picture&type=large";
            UserName = _userManager.FindByIdAsync(userId).Result.UserName;
        }

        public TwitterUser(string id, UserManager<ApplicationUser> _userManager)
        {
            UserImage = "https://graph.facebook.com/" + _userManager.FindByIdAsync(id).Result.FbProfile + "/?fields=picture&type=large";
            UserName = _userManager.FindByIdAsync(id).Result.UserName;
        }
    }
}
