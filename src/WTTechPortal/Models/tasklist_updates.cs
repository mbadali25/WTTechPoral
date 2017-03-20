using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WTTechPortal.Models
{
    [Table("asklist_updates")]
    public class tasklist_updates
    {
        [Key]
        public int id { get; set; }
        public int ticketid { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Ticket Update")]
        [DataType(DataType.MultilineText)]
        public string updatetext { get; set; }

        public DateTime createdate { get; set; }
        public string updateby { get; set; }

        [ForeignKey("ticketid")]
        public tasklist ticketids { get; set; }
    }
}
