using EPortalAdmin.Application.Repositories;
using EPortalAdmin.Core.Domain.Entities;
using EPortalAdmin.Core.Persistence.Repositories;

namespace EPortalAdmin.Persistence.Repositories
{
    public class EmailAuthenticatorRepository : EfRepositoryBase<EmailAuthenticator, EPortalAdminDbContext>, IEmailAuthenticatorRepository
    {
        public EmailAuthenticatorRepository(EPortalAdminDbContext context) : base(context)
        {
        }
    }
}
