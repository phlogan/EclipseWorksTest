using EclipseWorksTest.AppService.DTO.Project;
using EclipseWorksTest.CrossCutting.Validation;

namespace EclipseWorksTest.AppService.AppServices.Interfaces
{
    public interface IProjectAppService
    {
        IEnumerable<ProjectListDTO> GetProjectListDTOyUserId(int userId);
        EclipseValidationResult Add(ProjectManageDTO project);
        EclipseValidationResult Remove(int projectId);
    }
}
