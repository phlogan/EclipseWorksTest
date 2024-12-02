using EclipseWorksTest.CrossCutting.Enums.Users;
using EclipseWorksTest.Domain.Entities;
using EclipseWorksTest.Infra.Data.EntityConfig;
using Microsoft.EntityFrameworkCore;

namespace EclipseWorksTest.Infra.Data.DataContext
{
    public class EclipseDataContext : DbContext, IEclipseDataContext
    {
        public EclipseDataContext(DbContextOptions<EclipseDataContext> options) : base(options) { }

        public override int SaveChanges() => base.SaveChanges();
        public DbSet<User> Users { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectTask> Tasks { get; set; }
        public DbSet<ProjectTaskHistory> TaskHistories { get; set; }
        public DbSet<ProjectTaskComment> TaskComments { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfig());
            modelBuilder.ApplyConfiguration(new ProjectConfig());
            modelBuilder.ApplyConfiguration(new ProjectTaskConfig());
            modelBuilder.ApplyConfiguration(new ProjectTaskHistoryConfig());
            modelBuilder.ApplyConfiguration(new ProjectTaskCommentConfig());

            modelBuilder.Entity<User>().HasData(new User
            {
                Id = 1,
                Email = "baseuser@eclipse.local",
                Name = "Base User",
                Role = UserRole.Manager,
                SystemStatus = CrossCutting.Enums.System.SystemStatus.Active,
                WhenCreated = DateTime.MinValue                
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
