using Microsoft.EntityFrameworkCore;
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

