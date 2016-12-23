using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WTTechPortal.Models
{
    [Table("status_select")]
    public class status_select
    {
        [Key]
        public int statusid { get; set; }
        public string statusname { get; set; }
        
    }
}
