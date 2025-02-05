using Logbook.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logbook.DataAccess.Configurations;

public class StudentConfiguration : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        builder.Property(s => s.Diamonds).HasDefaultValue(0);
        builder.Property(s => s.Coins).HasDefaultValue(0);
    }
}