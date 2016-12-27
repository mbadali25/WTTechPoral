using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using WTTechPortal.Models;

namespace WTTechPortal.Data
{
    public class WttechportalDbContext : DbContext
    {
        
        public  DbSet<tasklist> tasklist { get; set; }
        public DbSet<hypervperf> hypervperf { get; set; }
        public DbSet <priority_select> priority_select { get; set; }
        public DbSet<status_select> status_select { get; set; }

        public DbSet<owner_select> owner_select { get; set; }

        public DbSet <site_config> site_config { get; set; }

        public DbSet<org_list> org_list { get; set; }

        public WttechportalDbContext(DbContextOptions<WttechportalDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {



            base.OnModelCreating(builder);


        }
    }
}
