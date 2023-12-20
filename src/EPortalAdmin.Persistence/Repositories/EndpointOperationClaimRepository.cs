using EPortalAdmin.Application.Repositories;
using EPortalAdmin.Core.Domain.Entities;
using EPortalAdmin.Core.Persistence.Repositories;

namespace EPortalAdmin.Persistence.Repositories
{
    public class EndpointOperationClaimRepository : 
        EfRepositoryBase<EndpointOperationClaim, EPortalAdminDbContext>, IEndpointOperationClaimRepository
    {
        public EndpointOperationClaimRepository(EPortalAdminDbContext context) : base(context)
        {
        }
    }
}
