using EPortalAdmin.Application.Repositories;
using EPortalAdmin.Core.Domain.Entities;
using EPortalAdmin.Core.Persistence.Repositories;

namespace EPortalAdmin.Persistence.Repositories
{
    public class MenuItemOperationClaimRepository : EfRepositoryBase<MenuItemOperationClaim, EPortalAdminDbContext>, IMenuItemOperationClaimRepository
    {
        public MenuItemOperationClaimRepository(EPortalAdminDbContext context) : base(context)
        {
        }
    }
}
