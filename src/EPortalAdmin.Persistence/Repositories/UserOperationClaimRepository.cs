using EPortalAdmin.Application.Repositories;
using EPortalAdmin.Core.Domain.Entities;
using EPortalAdmin.Core.Persistence.Repositories;

namespace EPortalAdmin.Persistence.Repositories
{
    public class UserOperationClaimRepository : EfRepositoryBase<UserOperationClaim, EPortalAdminDbContext>, IUserOperationClaimRepository
    {
        public UserOperationClaimRepository(EPortalAdminDbContext context) : base(context)
        {
        }
    }
}
