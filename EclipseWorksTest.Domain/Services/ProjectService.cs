using EclipseWorksTest.CrossCutting.Validation;
using EclipseWorksTest.Domain.Entities;
using EclipseWorksTest.Domain.Interfaces;
using EclipseWorksTest.Domain.Interfaces.Repositories;
using EclipseWorksTest.Domain.Interfaces.Services;

namespace EclipseWorksTest.Domain.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;
        public ProjectService(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public EclipseValidationResult Add(Project project)
        {
            var validationReuslt = ProjectValidate(project);
            if (validationReuslt.IsValid)
                _projectRepository.Add(project);

            return validationReuslt;
        }

        public EclipseValidationResult RemoveById(int projectId) => Remove(_projectRepository.GetById(projectId));

        private EclipseValidationResult Remove(Project project)
        {
            var validationReuslt = ProjectRemoveValidate(project);
            if (validationReuslt.IsValid)
                _projectRepository.Remove(project);

            return validationReuslt;
        }

        public IEnumerable<Project> GetAll() => _projectRepository.GetAllActive();

        public EclipseValidationResult ProjectValidate(Project project)
        {
            var result = new EclipseValidationResult();
            result.AddIf(project.Title.Length > 200, "O título deve ser menor que 200 caracteres");
            result.AddIf(project.Description.Length > 500, "A descrição deve ser menor que 500 caracteres");

            result.AddIf(string.IsNullOrWhiteSpace(project.Title), "O título deve ser informado");
            result.AddIf(string.IsNullOrWhiteSpace(project.Description), "A descrição deve ser informada");

            return result;
        }

        public EclipseValidationResult ProjectRemoveValidate(Project project)
        {
            var result = new EclipseValidationResult();
            var hasPendingTasks = _projectRepository.HasActivePendingÒrInProgressTasks(project.Id);
            result.AddIf(hasPendingTasks, "O projeto só poderá ser excluído após a conclusão de todas as tarefas");

            return result;
        }
    }
}
