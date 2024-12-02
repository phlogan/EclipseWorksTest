using EclipseWorksTest.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EclipseWorksTest.Infra.Data.EntityConfig
{
    public class ProjectTaskHistoryConfig : IEntityTypeConfiguration<ProjectTaskHistory>
    {
        public void Configure(EntityTypeBuilder<ProjectTaskHistory> builder)
        {
            builder.ToTable("TaskHistories");
            builder.HasKey(t => t.Id);
            builder.Property(t => t.NewValue).IsRequired().HasMaxLength(1000);
            builder.Property(t => t.OldValue).HasMaxLength(1000);
        }
    }
}
