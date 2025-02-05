using Logbook.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logbook.DataAccess.Configurations;

public class GroupTeacherConfiguration : IEntityTypeConfiguration<GroupTeacher>
{
    public void Configure(EntityTypeBuilder<GroupTeacher> builder)
    {
        builder.HasKey(gt => new { gt.GroupId, gt.TeacherId });
    }
}