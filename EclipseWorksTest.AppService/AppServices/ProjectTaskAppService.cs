using EclipseWorksTest.AppService.AppServices.Interfaces;
using EclipseWorksTest.AppService.DTO.Project;
using EclipseWorksTest.AppService.UoW;
using EclipseWorksTest.CrossCutting.Validation;
using EclipseWorksTest.Domain.Entities;
using EclipseWorksTest.Domain.Interfaces.Services;
using System.Text.Json;

namespace EclipseWorksTest.AppService.AppServices
{
    public class ProjectTaskAppService : BaseAppService, IProjectTaskAppService
    {
        private readonly IProjectTaskService _projectTaskService;

        public ProjectTaskAppService(IUnitOfWork uow, IProjectTaskService projectTaskService) : base(uow)
        {
            _projectTaskService = projectTaskService;
        }
        public EclipseValidationResult Add(ProjectTaskAddDTO projectTask)
        {
            var result = _projectTaskService.Add(new ProjectTask
            {
                Description = projectTask.Description,
                Title = projectTask.Title,
                Priority = projectTask.Priority,
                ProjectId = projectTask.ProjectId,
                Status = projectTask.Status
            });

            if (result.IsValid)
                SaveChanges();

            return result;
        }

        public EclipseValidationResult AddComment(ProjectTaskCommentAddDTO comment)
        {
            var projectTaskComment = new ProjectTaskComment
            {
                UserId = comment.UserId,
                Content = comment.Content,
                TaskId = comment.TaskId
            };

            var result = _projectTaskService.AddComment(projectTaskComment);
            if (result.IsValid)
            {
                _projectTaskService.RegisterComment(projectTaskComment);
                SaveChanges();
            }
            return result;
        }

        public IEnumerable<ProjectTaskListDTO> GetAllListDTOByProjectId(int projectId)
            => _projectTaskService.GetAllByProjectId(projectId).Select(e => new ProjectTaskListDTO(e));

        public EclipseValidationResult Remove(int projectTaskId)
        {
            var result = _projectTaskService.RemoveById(projectTaskId);
            if (result.IsValid)
                SaveChanges();
            return result;
        }

        public EclipseValidationResult Update(ProjectTaskPatchDTO projectTask)
        {
            var oldProjectTask = JsonSerializer.Deserialize<ProjectTask>(JsonSerializer.Serialize(_projectTaskService.GetById(projectTask.TaskId)));

            var projectTaskDomain = new Domain.DTO.Project.ProjectTaskPatchDTO
            {
                TaskId = projectTask.TaskId,
                Description = projectTask.Description,
                Priority = projectTask.Priority,
                ProjectId = projectTask.ProjectId,
                Status = projectTask.Status,
                Title = projectTask.Title,
                LoggedUserId = projectTask.LoggedUserId
            };
            var result = _projectTaskService.Update(projectTaskDomain);
            if (result.IsValid)
            {
                _projectTaskService.RegisterHistory(oldProjectTask, projectTaskDomain);

                SaveChanges();
            }

            return result;
        }
    }
}
