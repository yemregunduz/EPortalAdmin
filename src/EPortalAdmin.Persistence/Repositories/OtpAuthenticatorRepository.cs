using EPortalAdmin.Application.Repositories;
using EPortalAdmin.Core.Domain.Entities;
using EPortalAdmin.Core.Persistence.Repositories;

namespace EPortalAdmin.Persistence.Repositories
{
    public class OtpAuthenticatorRepository : EfRepositoryBase<OtpAuthenticator, EPortalAdminDbContext>, IOtpAuthenticatorRepository
    {
        public OtpAuthenticatorRepository(EPortalAdminDbContext context) : base(context)
        {
        }
    }
}
