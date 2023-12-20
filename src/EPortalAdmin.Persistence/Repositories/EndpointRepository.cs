using EPortalAdmin.Application.Repositories;
using EPortalAdmin.Core.Domain.Entities;
using EPortalAdmin.Core.Persistence.Repositories;

namespace EPortalAdmin.Persistence.Repositories
{
    public class EndpointRepository : EfRepositoryBase<Endpoint, EPortalAdminDbContext>, IEndpointRepository
    {
        public EndpointRepository(EPortalAdminDbContext context) : base(context)
        {
        }
    }
}
