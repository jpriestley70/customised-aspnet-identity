using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GenericBrand.Data.Models.Identity;

namespace GenericBrand.Data.DAL
{
    internal class ApplicationUserClaimConfiguration : IEntityTypeConfiguration<ApplicationUserClaim>
    {
        public void Configure(EntityTypeBuilder<ApplicationUserClaim> builder)
        {
            builder.ToTable("AspNetUserClaim", "gb_security");
            builder.Property(e => e.Id).HasColumnName("AspNetUserClaimId");
            builder.Property(e => e.UserId).HasColumnName("AspNetUserId");
        }
    }
}
