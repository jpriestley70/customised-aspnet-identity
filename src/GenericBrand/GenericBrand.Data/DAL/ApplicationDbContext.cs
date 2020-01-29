using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using GenericBrand.Data.Models.Identity;
using GenericBrand.Data.Models;

namespace GenericBrand.Data.DAL
{
    public sealed class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid, ApplicationUserClaim, ApplicationUserRole, ApplicationUserLogin, ApplicationRoleClaim, ApplicationUserToken>
    {
        #region Data Sets
        // ASP.NET Core Identity
        public DbSet<ApplicationRoleClaim> ApplicationRoleClaim { get; set; }
        public DbSet<ApplicationRole> ApplicationRole { get; set; }
        public DbSet<ApplicationUserClaim> ApplicationUserClaim { get; set; }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<ApplicationUserLogin> ApplicationUserLogin { get; set; }
        public DbSet<ApplicationUserRole> ApplicationUserRole { get; set; }
        public DbSet<ApplicationUserToken> ApplicationUserToken { get; set; }

        // Application

        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public ApplicationDbContext()
        {

        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="options"></param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        #endregion

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // ASP.NET Core Identity
            builder.ApplyConfiguration(new ApplicationRoleClaimConfiguration());
            builder.ApplyConfiguration(new ApplicationRoleConfiguration());
            builder.ApplyConfiguration(new ApplicationUserClaimConfiguration());
            builder.ApplyConfiguration(new ApplicationUserConfiguration());
            builder.ApplyConfiguration(new ApplicationUserLoginConfiguration());
            builder.ApplyConfiguration(new ApplicationUserRoleConfiguration());
            builder.ApplyConfiguration(new ApplicationUserTokenConfiguration());

            // Application
        }
    }
}
