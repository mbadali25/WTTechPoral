using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using WTTechPortal.Models;
using WTTechPortal.Models.Assests;
using WTTechPortal.Models.Inventory;
using WTTechPortal.Models.IPAM;
using WTTechPortal.Models.Jira;

namespace WTTechPortal.Data
{
    public class JiraDbContext : DbContext
    {
        
        public  DbSet<customfield> customfield { get; set; }
        public DbSet<customfieldoption> customfieldoption { get; set; }
        public DbSet <customfieldvalue> customfieldvalue { get; set; }
        public DbSet<issuestatus> issuestatus { get; set; }

        public DbSet<issuetype> issuetype { get; set; }

        public DbSet <jiraaction> jiraaction { get; set; }

        public DbSet<jiraissue> jiraissue { get; set; }

        public DbSet<project> project { get; set; }
        public DbSet<resolution> resolution { get; set; }
        public DbSet<worklog> worklog { get; set; }
        
        public JiraDbContext(DbContextOptions<JiraDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {



            base.OnModelCreating(builder);


        }
        
        
    }
}

