using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WTTechPortal.Models.Login
{
    public class aspnetuserroles
    {
        [Key]
        public string UserId { get; set; }
        [Key]
        public string RoleId { get; set; }

    }
}
