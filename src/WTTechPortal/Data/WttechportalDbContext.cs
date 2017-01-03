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

        public DbSet<assetinventory> assetinventory { get; set; }
        public DbSet<asset_model> asset_model { get; set; }
        public DbSet<asset_status> asset_status { get; set; }
        public DbSet<asset_opertaingsystem> asset_opertaingsystem { get; set; }
        public DbSet<asset_type> asset_type { get; set; }

        public DbSet<ipam> ipam { get; set; }
        public DbSet<hypervvms> hypervvms { get; set; }

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

