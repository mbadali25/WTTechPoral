﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WTTechPortal.Models.Jira
{
    public class project
    {
        [Key]
        public int ID { get; set; }

        public string pname { get; set; }

        public string pkey { get; set; }
    }
}
