using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WTTechPortal.Models
{
    public class org_list
    {
        public int id { get; set; }
        [Display(Name = "Orginzation Name")]
        public string orgname { get; set; }
        [Display(Name = "Email Contact ")]
        public string emailcontact { get; set; }
    }
}
