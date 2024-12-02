using EclipseWorksTest.CrossCutting.Enums.System;
using EclipseWorksTest.Domain.Entities;
using EclipseWorksTest.Domain.Interfaces.Repositories;
using EclipseWorksTest.Infra.Data.DataContext;
using Microsoft.EntityFrameworkCore;

namespace EclipseWorksTest.Infra.Data.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        protected readonly EclipseDataContext db;
        protected readonly DbSet<T> _dbSet;

        public BaseRepository(EclipseDataContext context)
        {
            db = context;
            _dbSet = db.Set<T>();
        }

        public IEnumerable<T> GetAllActive()
        {
            return _dbSet.Where(e => e.SystemStatus == SystemStatus.Active).ToList();
        }

        public T Add(T entity)
        {
            entity.SystemStatus = SystemStatus.Active;
            entity.WhenCreated = DateTime.UtcNow;
            return _dbSet.Add(entity).Entity;
        }

        public void Remove(T entity)
        {
            entity.WhenDeleted = DateTime.UtcNow;
            entity.SystemStatus = SystemStatus.Deleted;
        }

        public bool HasAny() => _dbSet.Any();

        public T? GetById(int id) => _dbSet.Find(id);
    }
}
