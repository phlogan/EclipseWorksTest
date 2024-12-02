using EclipseWorksTest.AppService.DTO.Project;
using EclipseWorksTest.CrossCutting.Validation;

namespace EclipseWorksTest.AppService.AppServices.Interfaces
{
    public interface IProjectTaskAppService
    {
        IEnumerable<ProjectTaskListDTO> GetAllListDTOByProjectId(int projectId);
        EclipseValidationResult Add(ProjectTaskAddDTO projectTask);
        EclipseValidationResult Update(ProjectTaskPatchDTO projectTask);
        EclipseValidationResult Remove(int projectTaskId);
        EclipseValidationResult AddComment(ProjectTaskCommentAddDTO comment);
    }
}
