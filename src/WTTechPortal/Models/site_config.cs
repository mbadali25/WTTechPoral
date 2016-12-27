using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WTTechPortal.Models
{
    public class site_config
    {
        public int id { get; set; }
        public string smtpserver { get; set; }
        public string smtpuser { get; set; }
        public string smtppassword { get; set; }
        public string sslenabled { get; set; }
        public int smtpport { get; set; }
        public string adminemail { get; set; }
        public string sendasemail { get; set; }
    }
}
