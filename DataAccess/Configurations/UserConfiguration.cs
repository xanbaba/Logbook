using System.Security.Cryptography;
using Bogus;
using Logbook.Entities;
using Logbook.Features.AuthFeature;
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
        builder.Property(u => u.UtcBornAt).IsRequired();

        // Bogus(Fake data)
        var fakeUsers = new Faker<User>()
            .RuleFor(u => u.FirstName, f => f.Name.FirstName())
            .RuleFor(u => u.FatherName, f => f.Name.FirstName())
            .RuleFor(u => u.Id, f => f.Random.Uuid())
            .RuleFor(u => u.Email, f => f.Internet.Email())
            .RuleFor(u => u.UtcBornAt, f => DateOnly.FromDateTime(f.Date.Past()))
            .RuleFor(u => u.LastName, f => f.Name.LastName())
            .RuleFor(u => u.UtcLastSeenAt, f => DateOnly.FromDateTime(f.Date.Past()))
            .RuleFor(u => u.Login, f => f.Internet.UserName())
            .RuleFor(u => u.PasswordHash,
                f => PasswordHasher.HashPassword(f.Internet.Password()))
            .RuleFor(u => u.Role, f => f.PickRandom<UserRole>()).Generate(1000);
        builder.HasData(fakeUsers);
    }
}