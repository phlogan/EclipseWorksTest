using EclipseWorksTest.Domain.Entities;

namespace EclipseWorksTest.Domain.Interfaces.Repositories
{
    public interface IProjectTaskRepository : IBaseRepository<ProjectTask>
    {
        ProjectTaskComment AddComment(ProjectTaskComment comment);
        ProjectTaskHistory AddHistory(ProjectTaskHistory history);
    }
}
