using EclipseWorksTest.CrossCutting.Validation;
using EclipseWorksTest.Domain.DTO.Project;
using EclipseWorksTest.Domain.Entities;

namespace EclipseWorksTest.Domain.Interfaces.Services
{
    public interface IProjectTaskService
    {
        ProjectTask? GetById(int taskId);
        IEnumerable<ProjectTask> GetAllByProjectId(int projectId);
        EclipseValidationResult Add(ProjectTask projectTask);
        EclipseValidationResult Update(ProjectTaskPatchDTO projectTask);
        void RegisterHistory(ProjectTask oldProjectTask, ProjectTaskPatchDTO projectTaskChanges);
        void RegisterComment(ProjectTaskComment comment);
        EclipseValidationResult RemoveById(int projectTaskId);
        EclipseValidationResult AddComment(ProjectTaskComment comment);
        EclipseValidationResult ValidateAdd(ProjectTask projectTask);
        EclipseValidationResult ValidateUpdate(ProjectTaskPatchDTO projectTask, ProjectTask projectTaskFromDb);
    }
}
