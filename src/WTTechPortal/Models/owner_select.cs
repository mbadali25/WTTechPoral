using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WTTechPortal.Models
{
    [Table("owner_select")]
    public class owner_select
    {
        [Key]
        public int ownerid { get; set; }
        public string ownername { get; set; }
        public string owneremail { get; set; }
        public string ownerorg { get; set; }
    }
}
