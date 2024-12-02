namespace EclipseWorksTest.Domain.Entities
{
    public class Project : BaseEntity
    {
        public Project()
        {
            Tasks = new List<ProjectTask>();
        }
        public string Title { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<ProjectTask> Tasks { get; set; }
    }
}
