using Microsoft.AspNetCore.Identity;

namespace WTTechPortal.Models
{
    public class WTIdentityUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }


    }
}
