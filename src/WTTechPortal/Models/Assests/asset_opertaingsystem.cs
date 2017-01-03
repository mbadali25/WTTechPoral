using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WTTechPortal.Models.Assests
{
    public class asset_opertaingsystem
    {
        public int id { get; set; }
        [Display(Name = "Operating System")]
        public string operatingsystem { get; set; }
    }
}
