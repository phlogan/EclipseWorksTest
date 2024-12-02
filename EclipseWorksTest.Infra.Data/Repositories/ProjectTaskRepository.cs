using EclipseWorksTest.Domain.Entities;
using EclipseWorksTest.Domain.Interfaces.Repositories;
using EclipseWorksTest.Infra.Data.DataContext;

namespace EclipseWorksTest.Infra.Data.Repositories
{
    public class ProjectTaskRepository : BaseRepository<ProjectTask>, IProjectTaskRepository
    {
        public ProjectTaskRepository(EclipseDataContext db) : base(db) { }

        public ProjectTaskComment AddComment(ProjectTaskComment comment)
        {
            comment.PreAdd();
            return db.TaskComments.Add(comment).Entity;
        }

        public ProjectTaskHistory AddHistory(ProjectTaskHistory history)
        {
            history.PreAdd();
            return db.TaskHistories.Add(history).Entity;
        }
    }
}
