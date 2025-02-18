using Logbook.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logbook.DataAccess.Configurations;

public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasOne(x => x.User).WithMany().HasForeignKey(x => x.UserId);
        builder.Property(x => x.UserId).IsRequired();
        builder.Property(x => x.UtcExpiresAt).IsRequired();
        builder.Property(x => x.Token).HasMaxLength(50).IsRequired();
    }
}