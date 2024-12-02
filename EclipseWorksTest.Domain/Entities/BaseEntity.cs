using EclipseWorksTest.CrossCutting.Enums.System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EclipseWorksTest.Domain.Entities
{
    public class BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime WhenCreated { get; set; }
        public DateTime? WhenUpdated { get; set; }
        public DateTime? WhenDeleted { get; set; }
        public SystemStatus SystemStatus { get; set; }

        public virtual void PreAdd()
        {
            WhenCreated = DateTime.UtcNow;
            SystemStatus = SystemStatus.Active;
        }
        public virtual void PreRemove()
        {
            WhenDeleted = DateTime.UtcNow;
            SystemStatus = SystemStatus.Deleted;
        }
    }
}
