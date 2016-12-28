using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WTTechPortal.Models.Login
{ 
    public class aspnetusers
    {
        [Key]
        public string Id { get; set;}
        public int AccessFailedCount { get; set; }
        public string ConcurrencyStamp { get; set; }
        public string Email { get; set; }
        public int EmailConfirmed { get; set; }
        public int LockoutEnabled  { get; set; }
        public DateTime LockoutEnd { get; set; }
        public string NormalizedEmail { get; set; }
        public string NormalizedUserName { get; set; }
        public string PasswordHash { get; set; }
        public string PhoneNumber { get; set; }
        public int PhoneNumberConfirmed { get; set; }
        public string SecurityStamp { get; set; }
        public int TwoFactorEnabled { get; set; }

        public string UserName { get; set; }
        public string EmailAddress { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
      
    }
}
