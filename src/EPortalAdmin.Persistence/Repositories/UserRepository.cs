using EPortalAdmin.Application.Repositories;
using EPortalAdmin.Core.Domain.Entities;
using EPortalAdmin.Core.Persistence.Repositories;

namespace EPortalAdmin.Persistence.Repositories
{
    public class UserRepository : EfRepositoryBase<User, EPortalAdminDbContext>, IUserRepository
    {
        public UserRepository(EPortalAdminDbContext context) : base(context)
        {
        }
    }
}
