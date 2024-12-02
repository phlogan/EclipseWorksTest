using EclipseWorksTest.AppService.AppServices.Interfaces;
using EclipseWorksTest.AppService.DTO.Project;
using EclipseWorksTest.AppService.UoW;
using EclipseWorksTest.CrossCutting.Validation;
using EclipseWorksTest.Domain.Entities;
using EclipseWorksTest.Domain.Interfaces.Services;

namespace EclipseWorksTest.AppService.AppServices
{
    public class ProjectAppService : BaseAppService, IProjectAppService
    {
        private readonly IProjectService _projectService;
        public ProjectAppService(IUnitOfWork uow, IProjectService projectService) : base(uow)
        {
            _projectService = projectService;
        }

        public EclipseValidationResult Add(ProjectManageDTO project)
        {
            var validationResult = _projectService.Add(new Project
            {
                Description = project.Description,
                UserId = project.UserId,
                Title = project.Title
            });
            if (validationResult.IsValid)
                SaveChanges();

            return validationResult;
        }
        public EclipseValidationResult Remove(int projectId)
        {
            var validationResult = _projectService.RemoveById(projectId);

            if (validationResult.IsValid)
                SaveChanges();

            return validationResult;
        }

        public IEnumerable<ProjectListDTO> GetProjectListDTOyUserId(int userId)
        {
            var result = _projectService.GetAll().Where(e => e.UserId == userId);

            return result.Select(e => new ProjectListDTO(e));
        }
    }
}
