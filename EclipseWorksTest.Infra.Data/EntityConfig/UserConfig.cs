using EclipseWorksTest.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EclipseWorksTest.Infra.Data.EntityConfig
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Name).IsRequired().HasMaxLength(100);
            builder.Property(u => u.Email).IsRequired().HasMaxLength(150);
            //builder.HasMany(u => u.Projects)
            //       .WithOne(p => p.User)
            //       .HasForeignKey(p => p.UserId)
            //       .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
