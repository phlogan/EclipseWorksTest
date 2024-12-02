using EclipseWorksTest.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EclipseWorksTest.Infra.Data.EntityConfig
{
    public class ProjectTaskCommentConfig : IEntityTypeConfiguration<ProjectTaskComment>
    {
        public void Configure(EntityTypeBuilder<ProjectTaskComment> builder)
        {
            builder.ToTable("TaskComments");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Content).IsRequired().HasMaxLength(500);
            //builder.HasOne(c => c.Task)
            //       .WithMany(t => t.Comments)
            //       .HasForeignKey(c => c.TaskId)
            //       .OnDelete(DeleteBehavior.Cascade);
            //builder.HasOne(c => c.User)
            //       .WithMany()
            //       .HasForeignKey(c => c.UserId)
            //       .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
