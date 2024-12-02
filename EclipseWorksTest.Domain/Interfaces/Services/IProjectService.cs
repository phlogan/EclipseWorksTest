using EclipseWorksTest.CrossCutting.Validation;
using EclipseWorksTest.Domain.Entities;

namespace EclipseWorksTest.Domain.Interfaces.Services
{
    public interface IProjectService
    {
        IEnumerable<Project> GetAll();
        EclipseValidationResult Add(Project project);
        EclipseValidationResult RemoveById(int projectId);
        EclipseValidationResult ProjectValidate(Project project);
        EclipseValidationResult ProjectRemoveValidate(Project project);
    }
}
