using EclipseWorksTest.Domain.Entities;

namespace EclipseWorksTest.Domain.Interfaces.Repositories
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        IEnumerable<T> GetAllActive();
        T Add(T entity);
        void Remove(T entity);
        bool HasAny();
        T? GetById(int id);
    }
}
