using EclipseWorksTest.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EclipseWorksTest.Infra.Data.EntityConfig
{
    public class ProjectTaskConfig : IEntityTypeConfiguration<ProjectTask>
    {
        public void Configure(EntityTypeBuilder<ProjectTask> builder)
        {
            builder.ToTable("Tasks");
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Title).IsRequired().HasMaxLength(150);
            builder.Property(t => t.Description).HasMaxLength(500);
            builder.Property(t => t.Status).IsRequired().HasMaxLength(20);
            builder.Property(t => t.Priority).IsRequired().HasMaxLength(10);
            //builder.HasOne(t => t.Project)
            //       .WithMany(p => p.Tasks)
            //       .HasForeignKey(t => t.ProjectId)
            //       .OnDelete(DeleteBehavior.Cascade);
            //builder.HasMany(t => t.History)
            //       .WithOne(h => h.Task)
            //       .HasForeignKey(h => h.TaskId)
            //       .OnDelete(DeleteBehavior.Cascade);
            //builder.HasMany(t => t.Comments)
            //       .WithOne(c => c.Task)
            //       .HasForeignKey(c => c.TaskId)
            //       .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
