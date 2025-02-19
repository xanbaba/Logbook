using System.Security.Cryptography;
using Bogus;
using Logbook.Entities;
using Logbook.Features.AuthFeature;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logbook.DataAccess.Configurations;

public class UserConfiguration(IConfiguration configuration) : IEntityTypeConfiguration<User>
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
        builder.Property(u => u.UtcBornAt);

        var defaultAdminLogin = configuration["DefaultAdmin:Login"];
        var defaultAdminPassword = configuration["DefaultAdmin:Password"];

        if (string.IsNullOrEmpty(defaultAdminLogin) || string.IsNullOrEmpty(defaultAdminPassword))
        {
            throw new ApplicationException("Default admin login or password are required. Try set DefaultAdmin:Login, DefaultAdmin:Password in appsettings.json");
        }

        builder.HasData(new User
        {
            Id = Guid.CreateVersion7(),
            FirstName = "DefaultAdmin",
            LastName = "DefaultAdmin",
            Login = defaultAdminLogin,
            PasswordHash = PasswordHasher.HashPassword(defaultAdminPassword),
            Role = UserRole.Admin
        });
    }
}