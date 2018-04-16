using System;
using Microsoft.AspNetCore.Identity;


namespace WTTechPortal.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public static implicit operator string(ApplicationUser v)
        {
            throw new NotImplementedException();
        }
    }
}
