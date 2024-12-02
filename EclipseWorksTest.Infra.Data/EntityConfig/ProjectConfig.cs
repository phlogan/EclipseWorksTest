using EclipseWorksTest.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EclipseWorksTest.Infra.Data.EntityConfig
{
    public class ProjectConfig : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.ToTable("Projects");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Title).IsRequired().HasMaxLength(200);
            builder.Property(p => p.Description).HasMaxLength(500);
            //builder.HasOne(p => p.User)
            //       .WithMany(u => u.Projects)
            //       .HasForeignKey(p => p.UserId)
            //       .OnDelete(DeleteBehavior.Cascade);
            //builder.HasMany(p => p.Tasks)
            //       .WithOne(t => t.Project)
            //       .HasForeignKey(t => t.ProjectId)
            //       .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
