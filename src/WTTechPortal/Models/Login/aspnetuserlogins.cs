using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WTTechPortal.Models.Login
{
    public class aspnetuserlogins
    {
        [Key, Column(Order = 0)]
        public string LoginProvider { get; set; }
        [Key, Column(Order = 1)]
        public string ProviderKey { get; set; }
        public string ProviderDisplayName { get; set; }
        public string UserId { get; set; }
    }
}
