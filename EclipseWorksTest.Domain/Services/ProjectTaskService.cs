using EclipseWorksTest.CrossCutting.Validation;
using EclipseWorksTest.Domain.DTO.Project;
using EclipseWorksTest.Domain.Entities;
using EclipseWorksTest.Domain.Interfaces.Repositories;
using EclipseWorksTest.Domain.Interfaces.Services;

namespace EclipseWorksTest.Domain.Services
{
    public class ProjectTaskService : IProjectTaskService
    {
        #region :: Consts
        private readonly uint MAX_TASKS_PER_PROJECT = 20;
        #endregion
        private readonly IProjectTaskRepository _projectTaskRepository;
        private readonly IProjectRepository _projectRepository;
        public ProjectTaskService(IProjectTaskRepository projectTaskRepository, IProjectRepository projectRepository)
        {
            _projectTaskRepository = projectTaskRepository;
            _projectRepository = projectRepository;
        }

        public EclipseValidationResult Add(ProjectTask projectTask)
        {
            var validationReuslt = ValidateAdd(projectTask);
            if (validationReuslt.IsValid)
                _projectTaskRepository.Add(projectTask);

            return validationReuslt;
        }

        public EclipseValidationResult AddComment(ProjectTaskComment comment)
        {
            var validationReuslt = CommentValidate(comment);
            if (validationReuslt.IsValid)
                _projectTaskRepository.AddComment(comment);

            return validationReuslt;
        }

        public ProjectTask? GetById(int taskId) => _projectTaskRepository.GetById(taskId);

        public IEnumerable<ProjectTask> GetAllByProjectId(int projectId) => _projectTaskRepository.GetAllActive();

        public EclipseValidationResult RemoveById(int projectTaskId) => Remove(_projectTaskRepository.GetById(projectTaskId));

        private EclipseValidationResult Remove(ProjectTask projectTask)
        {
            var result = new EclipseValidationResult();

            _projectTaskRepository.Remove(projectTask);

            return result;
        }

        public EclipseValidationResult Update(ProjectTaskPatchDTO projectTask)
        {
            var projectTaskDb = _projectTaskRepository.GetById(projectTask.TaskId);

            var validationReuslt = ValidateUpdate(projectTask, projectTaskDb);
            if (validationReuslt.IsValid)
            {
                projectTaskDb.Title = projectTask.Title;
                projectTaskDb.Description = projectTask.Description;
                projectTaskDb.Status = projectTask.Status;
            }

            return validationReuslt;
        }

        public void RegisterHistory(ProjectTask oldProjectTask, ProjectTaskPatchDTO projectTaskChanges)
        {
            if (oldProjectTask.Title != projectTaskChanges.Title)
                _projectTaskRepository.AddHistory(new ProjectTaskHistory(oldProjectTask.Id, nameof(oldProjectTask.Title), oldProjectTask.Title, projectTaskChanges.Title, projectTaskChanges.LoggedUserId));

            if (oldProjectTask.Description != projectTaskChanges.Description)
                _projectTaskRepository.AddHistory(new ProjectTaskHistory(oldProjectTask.Id, nameof(oldProjectTask.Description), oldProjectTask.Description, projectTaskChanges.Description, projectTaskChanges.LoggedUserId));

            if (oldProjectTask.Status != projectTaskChanges.Status)
                _projectTaskRepository.AddHistory(new ProjectTaskHistory(oldProjectTask.Id, nameof(oldProjectTask.Status), oldProjectTask.Status, projectTaskChanges.Status, projectTaskChanges.LoggedUserId));
        }

        public void RegisterComment(ProjectTaskComment comment) => 
            _projectTaskRepository.AddHistory(new ProjectTaskHistory(comment.TaskId, "Comment", "", comment.Content, comment.UserId));

        public EclipseValidationResult ValidateAdd(ProjectTask projectTask)
        {
            var result = new EclipseValidationResult();
            var projectTasksCount = _projectRepository.ActiveTasksCountByProjectId(projectTask.ProjectId);
            result.AddIf(projectTasksCount >= MAX_TASKS_PER_PROJECT, "O projeto atingiu o limite de tarefas");

            return result;
        }

        public EclipseValidationResult ValidateUpdate(ProjectTaskPatchDTO projectTask, ProjectTask projectTaskFromDb)
        {
            var result = new EclipseValidationResult();
            result.AddIf(projectTask.Priority != projectTaskFromDb.Priority, "O campo Prioridade não pode ser alterado");
            result.AddIf(projectTask.Title.Length > 150, "O título deve ser menor que 150 caracteres");
            result.AddIf(projectTask.Description.Length > 500, "A descrição deve ser menor que 500 caracteres");

            result.AddIf(string.IsNullOrWhiteSpace(projectTask.Title), "O título deve ser informado");
            result.AddIf(string.IsNullOrWhiteSpace(projectTask.Description), "A descrição deve ser informada");

            return result;
        }

        public EclipseValidationResult CommentValidate(ProjectTaskComment comment)
        {
            var result = new EclipseValidationResult();
            result.AddIf(comment.Content.Length > 500, "A descrição deve ser menor que 500 caracteres");
            result.AddIf(string.IsNullOrWhiteSpace(comment.Content), "O conteúdo deve ser informado");

            return result;
        }
    }
}
