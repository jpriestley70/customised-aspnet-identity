using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GenericBrand.Data.Models.Identity;

namespace GenericBrand.Data.DAL
{
    internal class ApplicationUserLoginConfiguration : IEntityTypeConfiguration<ApplicationUserLogin>
    {
        public void Configure(EntityTypeBuilder<ApplicationUserLogin> builder)
        {
            builder.ToTable("AspNetUserLogin", "gb_security");
            builder.Property(e => e.UserId).HasColumnName("AspNetUserId");
        }
    }
}
