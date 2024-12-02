using EclipseWorksTest.CrossCutting.Enums.ProjectTask;

namespace EclipseWorksTest.Domain.Entities
{
    public class ProjectTask : BaseEntity
    {
        public ProjectTask()
        {
            Comments = new List<ProjectTaskComment>();
            DueDate = DateTime.UtcNow.AddDays(30);
        }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public ProjectTaskStatus Status { get; set; }
        public ProjectTaskPriority Priority { get; set; }
        public int ProjectId { get; set; }
        public virtual Project Project { get; set; }
        public virtual ICollection<ProjectTaskComment> Comments { get; set; }
    }
}
