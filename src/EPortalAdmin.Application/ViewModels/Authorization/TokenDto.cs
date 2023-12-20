using EPortalAdmin.Core.Security.JWT;

namespace EPortalAdmin.Application.ViewModels.Authorization
{
    public class TokenDto
    {
        public AccessToken? AccessToken { get; set; }
        public TokenDto(AccessToken? accessToken)
        {
            AccessToken = accessToken;
        }
    }
}
