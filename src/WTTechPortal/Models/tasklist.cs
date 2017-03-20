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

        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? completedate     {   get; set;   }


        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Desired Complete Date")]
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? desiredcompdate { get; set; }


        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Update Date")]
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? updateddate { get; set; }


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
        public int org { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Hours Taken")]
        public decimal? hours { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Work Code")]
        public int? workcode { get; set; }



        [ForeignKey("workcode")]

        public workcodes workcodes { get; set; }

        [ForeignKey("priority")]
        public priority_select prioritites { get; set; }

        [ForeignKey("status")]
        public status_select statuses { get; set; }
        [ForeignKey("owner")]
        
        public owner_select owners { get; set; }

        [ForeignKey("id")]
        public tasklist_updates ticketupdates { get; set; }





    }

}

