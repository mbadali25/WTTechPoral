using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WTTechPortal.Models.Jira
{
    public class jiraaction
    {

        [Key]
        public int ID { get; set; }


        public int issueid { get; set; }

        public string AUTHOR { get; set; }

        public string actionbody { get; set; }
    }
}
