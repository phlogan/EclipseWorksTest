using EclipseWorksTest.CrossCutting.Enums.ProjectTask;
using EclipseWorksTest.CrossCutting.Enums.System;
using EclipseWorksTest.Domain.Entities;
using EclipseWorksTest.Domain.Interfaces.Repositories;
using EclipseWorksTest.Infra.Data.DataContext;

namespace EclipseWorksTest.Infra.Data.Repositories
{
    public class ProjectRepository : BaseRepository<Project>, IProjectRepository
    {
        public ProjectRepository(EclipseDataContext db) : base(db) { }

        public bool HasActivePendingÒrInProgressTasks(int projectId) => 
            db.Tasks
                .Where(e => e.ProjectId == projectId)
                .Any(e => e.SystemStatus == SystemStatus.Active && (e.Status == ProjectTaskStatus.Pending || e.Status == ProjectTaskStatus.InProgress));

        public int ActiveTasksCountByProjectId(int projectId) =>
            db.Tasks
                .Where(e => e.ProjectId == projectId && e.SystemStatus == SystemStatus.Active)
            .Count();
    }
}
