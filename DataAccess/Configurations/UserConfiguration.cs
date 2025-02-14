using Logbook.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logbook.DataAccess.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasIndex(u => u.Login).IsUnique();
        builder.HasIndex(u => u.Email).IsUnique();
        builder.HasIndex(u => u.FirstName).IsUnique(false);
        builder.HasIndex(u => u.Role).IsUnique(false);
        
        builder.Property(u => u.FirstName).HasMaxLength(50).IsRequired().IsUnicode();
        builder.Property(u => u.LastName).HasMaxLength(100).IsRequired().IsUnicode();
        builder.Property(u => u.FatherName).HasMaxLength(50).IsUnicode();
        builder.Property(u => u.Login).HasMaxLength(50).IsRequired().IsUnicode(false);
        builder.Property(u => u.PasswordHash).HasMaxLength(60).IsRequired().IsUnicode(false);
        builder.Property(u => u.Email).HasMaxLength(255).IsUnicode();
        builder.Property(u => u.Role).HasConversion<string>().HasMaxLength(20);
        builder.Property(u => u.BornAt).IsRequired();
    }
}