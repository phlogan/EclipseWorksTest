using EclipseWorksTest.AppService.UoW;
using EclipseWorksTest.Infra.Data.DataContext;

namespace EclipseWorksTest.AppService.UOW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IEclipseDataContext _context;
        public UnitOfWork(IEclipseDataContext context)
        {
            _context = context;
        }

        public int Commit() => _context.SaveChanges();
    }
}
