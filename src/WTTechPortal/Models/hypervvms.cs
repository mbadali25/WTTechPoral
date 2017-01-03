using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace WTTechPortal.Models
{
    public class hypervvms
    {
        [Key]
        public int id { get; set; }
        [Display(Name = "HostName")]
        public string host { get; set; }
        [Display(Name = "VM")]
        public string vm { get; set; }
        [Display(Name = "Stae")]
        public string state { get; set; }
        public int org { get; set; }
        [Display(Name = "Date Polled")]
        public DateTime date { get; set; }
        
        
        [ForeignKey("org")]
        public org_list orginzations { get; set; }
    }
}
