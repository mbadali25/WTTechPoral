using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WTTechPortal.Models;
using WTTechPortal.Models.Login;


namespace WTTechPortal.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, WTIdentityRole, string> 
    {

        public DbSet<aspnetusers> aspnetusers { get; set; }
        public DbSet<aspnetroleclaims> aspnetroleclaims { get; set; }
        public DbSet<aspnetroles> aspnetroles { get; set; }
        public DbSet<aspnetuserroles> aspnetuserroles { get; set; }
        public DbSet<aspnetuserlogins> aspnetuserlogins { get; set; }
        public DbSet<aspnetuserclaims> aspnetuserclaims { get; set; }
        public DbSet<aspnetusertokens> aspnetusertokens { get; set; }

        

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<aspnetuserlogins>()
                .HasKey("LoginProvider", "ProviderKey");
            builder.Entity<aspnetuserroles>()
                .HasKey("UserId", "RoleId");
        }
    }
}
