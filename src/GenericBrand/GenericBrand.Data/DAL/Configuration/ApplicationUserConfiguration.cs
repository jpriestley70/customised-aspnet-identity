using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GenericBrand.Data.Models.Identity;

namespace GenericBrand.Data.DAL
{
    internal class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.ToTable("AspNetUser", "gb_security");

            // Modifying existing properties
            builder.Property(e => e.Id).HasColumnName("AspNetUserId");

            builder.Property(e => e.UserName)
                .IsUnicode(true)
                .HasMaxLength(256);

            builder.Property(e => e.Email)
                .IsUnicode(true)
                .HasMaxLength(256);

            // Custom Properties
            builder.Property(e => e.FirstName)
                .IsUnicode(true)
                .IsRequired(false)
                .HasMaxLength(100);

            builder.Property(e => e.LastName)
                .IsUnicode(true)
                .IsRequired(false)
                .HasMaxLength(100);

            // References
            builder.HasMany(e => e.ApplicationUserRoles)
                .WithOne(e => e.ApplicationUser)
                .HasForeignKey(e => e.UserId);
        }
    }
}
