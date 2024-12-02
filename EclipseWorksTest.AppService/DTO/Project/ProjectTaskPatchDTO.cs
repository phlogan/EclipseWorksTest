namespace EclipseWorksTest.AppService.DTO.Project
{
    public class ProjectTaskPatchDTO : ProjectTaskAddDTO
    {
        public int TaskId { get; set; }
        public int LoggedUserId { get; set; }
    }
}
