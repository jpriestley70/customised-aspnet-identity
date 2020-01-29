using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GenericBrand.Data.Models.Identity;

namespace GenericBrand.Data.DAL
{
    internal class ApplicationUserRoleConfiguration : IEntityTypeConfiguration<ApplicationUserRole>
    {
        public void Configure(EntityTypeBuilder<ApplicationUserRole> builder)
        {
            builder.ToTable("AspNetUserRole", "gb_security");
            builder.Property(e => e.UserId).HasColumnName("AspNetUserId");
            builder.Property(e => e.RoleId).HasColumnName("AspNetRoleId");

            builder.HasOne(e => e.ApplicationUser)
                .WithMany(e => e.ApplicationUserRoles)
                .HasForeignKey(e => e.UserId);

            builder.HasOne(e => e.ApplicationRole)
                .WithMany(e => e.ApplicationUserRoles)
                .HasForeignKey(e => e.RoleId);
        }
    }
}
