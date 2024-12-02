using EclipseWorksTest.AppService.AppServices;
using EclipseWorksTest.AppService.AppServices.Interfaces;
using EclipseWorksTest.AppService.UoW;
using EclipseWorksTest.AppService.UOW;
using EclipseWorksTest.Domain.Interfaces.Repositories;
using EclipseWorksTest.Domain.Interfaces.Services;
using EclipseWorksTest.Domain.Services;
using EclipseWorksTest.Infra.Data.DataContext;
using EclipseWorksTest.Infra.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EclipseWorksTest.AppService.DI
{
    public static class InjectDependencies
    {
        public static void AddEclipseServices(this IServiceCollection services, IConfigurationManager configuration)
        {
            services.AddDbContext<IEclipseDataContext, EclipseDataContext>(options => options
            .UseLazyLoadingProxies()
            .UseSqlite(configuration.GetConnectionString("SQLite")));
            services.AddScoped<IProjectAppService, ProjectAppService>();
            services.AddScoped<IProjectTaskAppService, ProjectTaskAppService>();

            services.AddScoped<IProjectService, ProjectService>();
            services.AddScoped<IProjectTaskService, ProjectTaskService>();

            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<IProjectTaskRepository, ProjectTaskRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IEclipseDataContext, EclipseDataContext>();
        }

        public static void ApplyMigrations(this IServiceProvider services)
        {
            using (var scope = services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<EclipseDataContext>();
                db.Database.Migrate();
            }
        }
    }
}
