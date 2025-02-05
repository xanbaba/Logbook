using Logbook.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logbook.DataAccess.Configurations;

public class GroupConfiguration : IEntityTypeConfiguration<Group>
{
    public void Configure(EntityTypeBuilder<Group> builder)
    {
        builder
            .HasOne(g => g.Department)
            .WithMany(d => d.Groups)
            .HasForeignKey(g => g.DepartmentId);
    }
}