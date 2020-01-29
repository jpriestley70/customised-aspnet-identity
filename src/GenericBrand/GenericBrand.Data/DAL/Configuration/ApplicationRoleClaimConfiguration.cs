using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GenericBrand.Data.Models.Identity;

namespace GenericBrand.Data.DAL
{
    internal class ApplicationRoleClaimConfiguration : IEntityTypeConfiguration<ApplicationRoleClaim>
    {
        public void Configure(EntityTypeBuilder<ApplicationRoleClaim> builder)
        {
            builder.ToTable("AspNetRoleClaim", "gb_security");
            builder.Property(e => e.Id).HasColumnName("AspNetRoleClaimId");
            builder.Property(e => e.RoleId).HasColumnName("AspNetRoleId");
        }
    }
}
