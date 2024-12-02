using EclipseWorksTest.Domain.Entities;

namespace EclipseWorksTest.Domain.Interfaces.Repositories
{
    public interface IProjectRepository : IBaseRepository<Project> 
    {
        bool HasActivePendingÒrInProgressTasks(int projectId);
        int ActiveTasksCountByProjectId(int projectId);
    }
}
