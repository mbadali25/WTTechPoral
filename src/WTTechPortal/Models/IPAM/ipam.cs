using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WTTechPortal.Models.IPAM
{
    public class ipam
    {
        [Key]
        public int id { get; set; }
        [Display(Name = "IP")]
        public string ip { get; set; }
        [Display(Name = "Ping")]
        public string ping { get; set; }
        [Display(Name = "DNS HostName")]
        public string hostname { get; set; }
        [Display(Name = "Ports")]
        public string ports { get; set; }
        public int org { get; set; }
        [Display(Name = "Date Last Scanned")]
        public DateTime date { get; set; }
        [Display(Name = "State")]
        public string state { get; set; }

        [ForeignKey("org")]
        public org_list orginzation { get; set; }

    }
}
