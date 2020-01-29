using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GenericBrand.Data.Models.Identity;

namespace GenericBrand.Data.DAL
{
    internal class ApplicationRoleConfiguration : IEntityTypeConfiguration<ApplicationRole>
    {
        public void Configure(EntityTypeBuilder<ApplicationRole> builder)
        {
            builder.ToTable("AspNetRole", "gb_security");
            builder.Property(e => e.Id).HasColumnName("AspNetRoleId");

            builder.HasMany(e => e.ApplicationUserRoles)
                .WithOne(e => e.ApplicationRole)
                .HasForeignKey(e => e.RoleId);
        }
    }
}
