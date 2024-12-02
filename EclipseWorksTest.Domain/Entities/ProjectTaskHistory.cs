namespace EclipseWorksTest.Domain.Entities
{
    public class ProjectTaskHistory : BaseEntity
    {
        public ProjectTaskHistory()
        {
            
        }
        public ProjectTaskHistory(int projectTaskId, string propertyName, object? newValue, object? oldValue, int userId)
        {
            ProjectTaskId = projectTaskId;
            PropertyName = propertyName;
            OldValue = oldValue?.ToString();
            NewValue = newValue?.ToString();
            UserId = userId;
        }
        public int ProjectTaskId { get; set; }
        public virtual ProjectTask ProjectTask { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public string PropertyName { get; set; }
        public string? OldValue { get; set; }
        public string? NewValue { get; set; }
    }
}
