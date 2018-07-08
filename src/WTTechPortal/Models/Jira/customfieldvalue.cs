using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WTTechPortal.Models.Jira
{
    public class customfieldvalue
    {
        
        
        public int ID { get; set; }

        [Key]
        public int ISSUE { get; set; }
        
        public int CUSTOMFIELD { get; set; }

        public int? STRINGVALUE { get; set; }

        [ForeignKey("STRINGVALUE")]
        public customfieldoption customfieldoptions { get; set; }

        
    }
}
