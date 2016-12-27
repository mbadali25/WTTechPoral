using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WTTechPortal.Models
{
    [Table("tasklist")]
    public class tasklist
    {
        
        [Key]
        public int id { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Task")]
        public string task { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Current Action Item")]
        public string actionitem { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Status")]
        public int status { get;  set; }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Completed Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString =
        "{0:MM-dd-yyyy}",
       ApplyFormatInEditMode = true)]
        public DateTime? completedate { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Resolution")]
        public string comments { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Owner")]
        public int owner { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("priority")]
        [Display(Name = "Priority")]
        public int priority { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Organization")]
        public string org { get; set; }
       

        [ForeignKey("priority")]
        public priority_select prioritites { get; set; }

        [ForeignKey("status")]
        public status_select statuses { get; set; }
        [ForeignKey("owner")]
        
        
        public owner_select owners { get; set; }





    }

}

