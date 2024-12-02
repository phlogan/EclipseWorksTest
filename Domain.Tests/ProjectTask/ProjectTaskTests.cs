using EclipseWorksTest.CrossCutting.Enums.ProjectTask;
using EclipseWorksTest.Domain.DTO.Project;
using EclipseWorksTest.Domain.Entities;
using EclipseWorksTest.Domain.Interfaces.Repositories;
using EclipseWorksTest.Domain.Services;
using Moq;

namespace Domain.Tests.ProjectTask
{
    public class ProjectTaskTests
    {
        private const int MAX_TASKS_PER_PROJECT = 20;

        [Fact]
        public void ValidateAdd_ShouldReturnError_WhenTaskCountExceedsLimit()
        {
            var projectTask = new EclipseWorksTest.Domain.Entities.ProjectTask();
            var projectRepository = new Mock<IProjectRepository>();
            var projectTaskRepository = new Mock<IProjectTaskRepository>();
            projectRepository.Setup(repo => repo.ActiveTasksCountByProjectId(It.IsAny<int>())).Returns(MAX_TASKS_PER_PROJECT + 1);

            var projectService = new ProjectTaskService(projectTaskRepository.Object, projectRepository.Object);

            var result = projectService.ValidateAdd(projectTask);

            Assert.False(result.IsValid);
            Assert.Contains(result.ErrorList, e => e == "O projeto atingiu o limite de tarefas");
        }

        [Fact]
        public void ValidateUpdate_ShouldReturnError_WhenPriorityIsModified()
        {
            var newTask = new ProjectTaskPatchDTO { Priority = ProjectTaskPriority.High };
            var existingTask = new EclipseWorksTest.Domain.Entities.ProjectTask { Priority = ProjectTaskPriority.Low };
            var projectRepository = new Mock<IProjectRepository>();
            var projectTaskRepository = new Mock<IProjectTaskRepository>();
            var projectService = new ProjectTaskService(projectTaskRepository.Object, projectRepository.Object);

            var result = projectService.ValidateUpdate(newTask, existingTask);

            Assert.False(result.IsValid);
            Assert.Contains(result.ErrorList, e => e == "O campo Prioridade não pode ser alterado");
        }

        [Fact]
        public void ValidateUpdate_ShouldReturnError_WhenTitleExceeds150Characters()
        {
            var projectTask = new ProjectTaskPatchDTO { Title = new string('A', 151) };
            var existingTask = new EclipseWorksTest.Domain.Entities.ProjectTask();
            var projectRepository = new Mock<IProjectRepository>();
            var projectTaskRepository = new Mock<IProjectTaskRepository>();
            var projectService = new ProjectTaskService(projectTaskRepository.Object, projectRepository.Object);

            var result = projectService.ValidateUpdate(projectTask, existingTask);

            Assert.False(result.IsValid);
            Assert.Contains(result.ErrorList, e => e == "O título deve ser menor que 150 caracteres");
        }

        [Fact]
        public void ValidateUpdate_ShouldReturnError_WhenDescriptionExceeds500Characters()
        {
            var projectTask = new ProjectTaskPatchDTO { Description = new string('B', 501) };
            var existingTask = new EclipseWorksTest.Domain.Entities.ProjectTask();
            var projectRepository = new Mock<IProjectRepository>();
            var projectTaskRepository = new Mock<IProjectTaskRepository>();
            var projectService = new ProjectTaskService(projectTaskRepository.Object, projectRepository.Object);

            var result = projectService.ValidateUpdate(projectTask, existingTask);

            Assert.False(result.IsValid);
            Assert.Contains(result.ErrorList, e => e == "A descrição deve ser menor que 500 caracteres");
        }

        [Fact]
        public void ValidateUpdate_ShouldReturnError_WhenTitleIsEmptyOrWhitespace()
        {
            var projectTask = new ProjectTaskPatchDTO { Title = "  " };
            var existingTask = new EclipseWorksTest.Domain.Entities.ProjectTask();
            var projectRepository = new Mock<IProjectRepository>();
            var projectTaskRepository = new Mock<IProjectTaskRepository>();
            var projectService = new ProjectTaskService(projectTaskRepository.Object, projectRepository.Object);

            var result = projectService.ValidateUpdate(projectTask, existingTask);

            Assert.False(result.IsValid);
            Assert.Contains(result.ErrorList, e => e == "O título deve ser informado");
        }

        [Fact]
        public void ValidateUpdate_ShouldReturnError_WhenDescriptionIsEmptyOrWhitespace()
        {
            var projectTask = new ProjectTaskPatchDTO { Description = "  " };
            var existingTask = new EclipseWorksTest.Domain.Entities.ProjectTask();
            var projectRepository = new Mock<IProjectRepository>();
            var projectTaskRepository = new Mock<IProjectTaskRepository>();
            var projectService = new ProjectTaskService(projectTaskRepository.Object, projectRepository.Object);

            var result = projectService.ValidateUpdate(projectTask, existingTask);

            Assert.False(result.IsValid);
            Assert.Contains(result.ErrorList, e => e == "A descrição deve ser informada");
        }

        [Fact]
        public void CommentValidate_ShouldReturnError_WhenContentExceeds500Characters()
        {
            var comment = new ProjectTaskComment { Content = new string('C', 501) };
            var projectRepository = new Mock<IProjectRepository>();
            var projectTaskRepository = new Mock<IProjectTaskRepository>();
            var projectService = new ProjectTaskService(projectTaskRepository.Object, projectRepository.Object);

            var result = projectService.CommentValidate(comment);

            Assert.False(result.IsValid);
            Assert.Contains(result.ErrorList, e => e == "A descrição deve ser menor que 500 caracteres");
        }

        [Fact]
        public void CommentValidate_ShouldReturnError_WhenContentIsEmptyOrWhitespace()
        {
            var comment = new ProjectTaskComment { Content = "  " };
            var projectRepository = new Mock<IProjectRepository>();
            var projectTaskRepository = new Mock<IProjectTaskRepository>();
            var projectService = new ProjectTaskService(projectTaskRepository.Object, projectRepository.Object);

            var result = projectService.CommentValidate(comment);

            Assert.False(result.IsValid);
            Assert.Contains(result.ErrorList, e => e == "O conteúdo deve ser informado");
        }

        [Fact]
        public void CommentValidate_ShouldPass_WhenContentIsValid()
        {
            var comment = new ProjectTaskComment { Content = "Valid comment" };
            var projectRepository = new Mock<IProjectRepository>();
            var projectTaskRepository = new Mock<IProjectTaskRepository>();
            var projectService = new ProjectTaskService(projectTaskRepository.Object, projectRepository.Object);

            var result = projectService.CommentValidate(comment);

            Assert.True(result.IsValid);
        }
    }

}
