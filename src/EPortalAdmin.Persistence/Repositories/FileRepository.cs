using EPortalAdmin.Application.Repositories;
using EPortalAdmin.Core.Persistence.Repositories;

namespace EPortalAdmin.Persistence.Repositories
{
    public class FileRepository : EfRepositoryBase<Core.FileStorage.File, EPortalAdminDbContext>, IFileRepository
    {
        public FileRepository(EPortalAdminDbContext context) : base(context)
        {
        }
    }
}
