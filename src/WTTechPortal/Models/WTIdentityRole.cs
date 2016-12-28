using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace WTTechPortal.Models
{
    public class WTIdentityRole : IdentityRole
    {
        public string Description { get; set; }


    }

}
