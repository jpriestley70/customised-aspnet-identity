using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GenericBrand.Data.Models.Identity;

namespace GenericBrand.Data.DAL
{
    internal class ApplicationUserTokenConfiguration : IEntityTypeConfiguration<ApplicationUserToken>
    {
        public void Configure(EntityTypeBuilder<ApplicationUserToken> builder)
        {
            builder.ToTable("AspNetUserToken", "gb_security");
            builder.Property(e => e.UserId).HasColumnName("AspNetUserId");
        }
    }
}
