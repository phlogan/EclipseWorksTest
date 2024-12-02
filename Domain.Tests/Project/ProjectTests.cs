using EclipseWorksTest.CrossCutting.Enums.ProjectTask;
using EclipseWorksTest.Domain.Entities;
using EclipseWorksTest.Domain.Interfaces.Repositories;
using EclipseWorksTest.Domain.Services;
using Moq;

namespace Domain.Tests.Services
{
    public class ProjectTests
    {
        [Fact]
        public void Project_CannotBeRemoved_WhenTasksArePending()
        {
            var project = new Project { Id = 1, Title = "Project 1", Tasks = { new EclipseWorksTest.Domain.Entities.ProjectTask { Title = "Pending Task", Status = ProjectTaskStatus.Pending } } };

            var projectRepository = new Mock<IProjectRepository>();
            projectRepository.Setup(repo => repo.HasActivePendingÒrInProgressTasks(project.Id)).Returns(true);
            var projectService = new ProjectService(projectRepository.Object);

            var result = projectService.ProjectRemoveValidate(project);

            Assert.False(result.IsValid);
            Assert.Contains(result.ErrorList, e => e == "O projeto só poderá ser excluído após a conclusão de todas as tarefas");
        }

        [Fact]
        public void ProjectValidate_ShouldReturnError_WhenTitleExceeds200Characters()
        {
            var project = new Project
            {
                Title = new string('A', 201),
                Description = "Description"
            };
            var projectRepository = new Mock<IProjectRepository>();
            var projectService = new ProjectService(projectRepository.Object);

            var result = projectService.ProjectValidate(project);

            Assert.False(result.IsValid);
            Assert.Contains(result.ErrorList, e => e == "O título deve ser menor que 200 caracteres");
        }

        [Fact]
        public void ProjectValidate_ShouldReturnError_WhenDescriptionExceeds500Characters()
        {
            var project = new Project
            {
                Title = "Title",
                Description = new string('B', 501)
            };
            var projectRepository = new Mock<IProjectRepository>();
            var projectService = new ProjectService(projectRepository.Object);

            var result = projectService.ProjectValidate(project);

            Assert.False(result.IsValid);
            Assert.Contains(result.ErrorList, e => e == "A descrição deve ser menor que 500 caracteres");
        }

        [Fact]
        public void ProjectValidate_ShouldReturnError_WhenTitleIsEmptyOrWhitespace()
        {
            var project = new Project
            {
                Title = "  ",
                Description = "Description"
            };
            var projectRepository = new Mock<IProjectRepository>();
            var projectService = new ProjectService(projectRepository.Object);

            var result = projectService.ProjectValidate(project);

            Assert.False(result.IsValid);
            Assert.Contains(result.ErrorList, e => e == "O título deve ser informado");
        }

        [Fact]
        public void ProjectValidate_ShouldReturnError_WhenDescriptionIsEmptyOrWhitespace()
        {
            var project = new Project
            {
                Title = "Title",
                Description = "  "
            };
            var projectRepository = new Mock<IProjectRepository>();
            var projectService = new ProjectService(projectRepository.Object);

            var result = projectService.ProjectValidate(project);

            Assert.False(result.IsValid);
            Assert.Contains(result.ErrorList, e => e == "A descrição deve ser informada");
        }

        [Fact]
        public void ProjectValidate_ShouldPass_WhenTitleAndDescriptionAreValid()
        {
            var project = new Project
            {
                Title = "Valid title",
                Description = "Valid description"
            };
            var projectRepository = new Mock<IProjectRepository>();
            var projectService = new ProjectService(projectRepository.Object);

            var result = projectService.ProjectValidate(project);

            Assert.True(result.IsValid);
        }
    }
}
