using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace KuetOverflow.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public string FbProfile { get; set; }
        public string ImageUrl { get; set; }
    }
}
