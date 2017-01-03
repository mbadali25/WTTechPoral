using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WTTechPortal.Models.Assests
{
    public class asset_type
    {
        public int id { get; set; }
        [Display(Name = "Asset Type")]
        public string assettype { get; set; }
    }
}
