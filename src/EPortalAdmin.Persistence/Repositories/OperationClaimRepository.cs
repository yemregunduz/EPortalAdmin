using EPortalAdmin.Application.Repositories;
using EPortalAdmin.Core.Domain.Entities;
using EPortalAdmin.Core.Persistence.Repositories;

namespace EPortalAdmin.Persistence.Repositories
{
    public class OperationClaimRepository : EfRepositoryBase<OperationClaim, EPortalAdminDbContext>, IOperationClaimRepository
    {
        public OperationClaimRepository(EPortalAdminDbContext context) : base(context)
        {
        }
    }
}
