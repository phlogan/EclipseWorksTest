using EclipseWorksTest.CrossCutting.Enums.ProjectTask;

namespace EclipseWorksTest.AppService.DTO.Project
{
    public class ProjectTaskAddDTO
    {
        public ProjectTaskAddDTO() { }
        public string Title { get; set; }
        public string Description { get; set; }
        public ProjectTaskStatus Status { get; set; }
        public ProjectTaskPriority Priority { get; set; }
        public int ProjectId { get; set; }
    }
}
