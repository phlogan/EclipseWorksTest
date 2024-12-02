using EclipseWorksTest.Domain.Entities;
using EclipseWorksTest.Domain.Interfaces.Repositories;
using EclipseWorksTest.Infra.Data.DataContext;

namespace EclipseWorksTest.Infra.Data.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(EclipseDataContext context) : base(context) { }
    }
}
