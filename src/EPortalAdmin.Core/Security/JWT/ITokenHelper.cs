using EPortalAdmin.Core.Domain.Entities;

namespace EPortalAdmin.Core.Security.JWT
{

    public interface ITokenHelper
    {
        AccessToken CreateToken(User user, IList<OperationClaim> operationClaims);

        RefreshToken CreateRefreshToken(User user, string ipAddress);
    }
}
