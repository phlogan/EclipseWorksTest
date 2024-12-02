using EclipseWorksTest.CrossCutting.Enums.System;
namespace EclipseWorksTest.AppService.DTO.Project
{
    public class ProjectListDTO
    {
        public ProjectListDTO(Domain.Entities.Project project)
        {
            Id = project.Id;
            Title = project.Title;
            Description = project.Description;
            UserId = project.UserId;
            Tasks = project.Tasks.Select(e => new ProjectTaskListDTO(e)).ToList();
            SystemStatus = project.SystemStatus;
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
        public IEnumerable<ProjectTaskListDTO> Tasks { get; set; }
        public SystemStatus SystemStatus { get; set; }
    }
}
