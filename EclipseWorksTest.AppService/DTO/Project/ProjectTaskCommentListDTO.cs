using EclipseWorksTest.CrossCutting.Enums.System;
using EclipseWorksTest.Domain.Entities;

namespace EclipseWorksTest.AppService.DTO.Project
{
    public class ProjectTaskCommentListDTO
    {
        public ProjectTaskCommentListDTO(ProjectTaskComment comment)
        {
            Id = comment.Id;
            Content = comment.Content;
            TaskId = comment.TaskId;
            UserId = comment.UserId;
            SystemStatus = comment.SystemStatus;
        }
        public int Id { get; set; }
        public string Content { get; set; }
        public int TaskId { get; set; }
        public int UserId { get; set; }
        public SystemStatus SystemStatus { get; set; }
    }
}
