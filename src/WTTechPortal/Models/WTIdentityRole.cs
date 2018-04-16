using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace WTTechPortal.Models
{
    public class WTIdentityRole : IdentityRole
    {
        public string Description { get; set; }
        [Display(Name = "Orginzation")]
        public int org { get; set; }
        [ForeignKey("org")]
        public org_list orgs { get; set; }

    }

}
