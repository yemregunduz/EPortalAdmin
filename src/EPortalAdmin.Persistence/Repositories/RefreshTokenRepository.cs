using EPortalAdmin.Application.Repositories;
using EPortalAdmin.Core.Domain.Entities;
using EPortalAdmin.Core.Persistence.Repositories;

namespace EPortalAdmin.Persistence.Repositories
{
    public class RefreshTokenRepository : EfRepositoryBase<RefreshToken, EPortalAdminDbContext>, IRefreshTokenRepository
    {
        public RefreshTokenRepository(EPortalAdminDbContext context) : base(context)
        {
        }
    }
}
