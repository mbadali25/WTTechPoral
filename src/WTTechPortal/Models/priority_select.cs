using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WTTechPortal.Models
{
    
    public class priority_select 
    {

        [Key]
        [Column("priorityid")]
        public int priorityid { get; set; }
        public string priorityname { get; set; }
        





    }
}
