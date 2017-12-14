using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WTTechPortal.Models.Jira
{
    public class worklog
    {
        
        public int ID { get; set; }

        [Key]
        public int issueid { get; set; }

        public string worklogbody { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy hh:mm}")]
        public DateTime CREATED { get; set; }



    }
}
