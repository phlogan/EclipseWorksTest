using EclipseWorksTest.CrossCutting.Enums.ProjectTask;
using EclipseWorksTest.CrossCutting.Enums.System;

namespace EclipseWorksTest.AppService.DTO.Project
{
    public class ProjectTaskListDTO
    {
        public ProjectTaskListDTO(Domain.Entities.ProjectTask projectTask)
        {
            Id = projectTask.Id;
            Title = projectTask.Title;
            Description = projectTask.Description;
            Status = projectTask.Status;
            Priority = projectTask.Priority;
            ProjectId = projectTask.ProjectId;
            Comments = projectTask.Comments.Select(e => new ProjectTaskCommentListDTO(e)).ToList();
            SystemStatus = projectTask.SystemStatus;
            DueDate = projectTask.DueDate;
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public ProjectTaskStatus Status { get; set; }
        public ProjectTaskPriority Priority { get; set; }
        public int ProjectId { get; set; }
        public IEnumerable<ProjectTaskCommentListDTO> Comments { get; set; }
        public SystemStatus SystemStatus { get; set; }
    }
}
