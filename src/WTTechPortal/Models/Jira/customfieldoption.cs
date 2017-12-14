using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WTTechPortal.Models.Jira
{
    public class customfieldoption
    {

        [Key]
        public int ID { get; set; }
        
        public int customfield { get; set; }
        
        public string customvalue { get; set; }
    }
}
