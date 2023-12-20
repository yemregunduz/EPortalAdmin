using EPortalAdmin.Application.Repositories;
using EPortalAdmin.Core.Domain.Entities;
using EPortalAdmin.Core.Persistence.Repositories;

namespace EPortalAdmin.Persistence.Repositories
{
    public class MenuItemRepository : EfRepositoryBase<MenuItem, EPortalAdminDbContext>, IMenuItemRepository
    {
        public MenuItemRepository(EPortalAdminDbContext context) : base(context)
        {
        }
    }
}
