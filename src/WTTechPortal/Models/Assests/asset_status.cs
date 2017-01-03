using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WTTechPortal.Models.Assests
{
    public class asset_status
    {
        public int id { get; set; }
        [Display(Name = "Asset Status")]
        public string assetstatus { get; set; }
    }
}
