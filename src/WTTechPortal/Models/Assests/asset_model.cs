using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WTTechPortal.Models.Assests
{
    public class asset_model
    {
        public int id { get; set; }
        [Display(Name = "Brand")]
        public string model { get; set; }
    }
}
