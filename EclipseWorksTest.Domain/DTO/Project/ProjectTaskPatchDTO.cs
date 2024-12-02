using EclipseWorksTest.CrossCutting.Enums.ProjectTask;

namespace EclipseWorksTest.Domain.DTO.Project
{
    public class ProjectTaskPatchDTO
    {
        public ProjectTaskPatchDTO()
        {
            Status = ProjectTaskStatus.InProgress;
            Priority = ProjectTaskPriority.Low;
            Title = "";
            Description = "";
        }
        public int TaskId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public ProjectTaskStatus Status { get; set; }
        public ProjectTaskPriority Priority { get; set; }
        public int ProjectId { get; set; }
        public int LoggedUserId { get; set; }
    }
}
