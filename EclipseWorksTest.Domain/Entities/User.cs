using EclipseWorksTest.CrossCutting.Enums.Users;

namespace EclipseWorksTest.Domain.Entities
{
    public class User : BaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public virtual ICollection<Project> Projects { get; set; }
        public UserRole Role { get; set; }
    }
}
