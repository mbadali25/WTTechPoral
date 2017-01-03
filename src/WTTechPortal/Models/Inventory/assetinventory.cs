using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WTTechPortal.Models.Assests;

using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WTTechPortal.Models.Inventory
{
    public class assetinventory
    {
        [Key]
        public int id { get; set; }
        [Display(Name = "Asset Type")]
        public int assettype { get; set; }
        [Display(Name = "Asset Orginzation")]
        public int org { get; set; }
        [Display(Name = "Asset ID")]
        public string assetid { get; set; }
        [Display(Name = "HDD Size")]
        public int hddsize { get; set; }
        [Display(Name = "Memory Installed")]
        public int memory { get; set; }
        [Display(Name = "Asset Status")]
        public int ready { get; set; }
        [Display(Name = "Notes")]
        public string notes { get; set; }
        [Display(Name = "Operating System")]
        public int operatingsystem { get; set; }
        [Display(Name = "Model")]
        public string model { get; set; }
        [Display(Name = "Brand")]
        public int brand { get; set; }
        [Display(Name = "CPU Speed")]
        public decimal cpuspd { get; set; }
        [Display(Name = "CPU Cores")]
        public int cpucores { get; set; }
        [Display(Name = "CPU Model")]
        public string cpumodel { get; set; }

        [ForeignKey("brand")]
        public asset_model brands { get; set; }

        [ForeignKey("org")]
        public org_list orginzation { get; set; }

        [ForeignKey("ready")]
        public asset_status status { get; set; }

        [ForeignKey("assettype")]
        public asset_type assettypes { get; set; }

        [ForeignKey("operatingsystem")]
        public asset_opertaingsystem operatingsystems { get; set; }


    }
}
