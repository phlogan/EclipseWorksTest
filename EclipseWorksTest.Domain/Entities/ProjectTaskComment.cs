namespace EclipseWorksTest.Domain.Entities
{
    public class ProjectTaskComment : BaseEntity
    {
        public string Content { get; set; }
        public int TaskId { get; set; }
        public virtual ProjectTask Task { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}
