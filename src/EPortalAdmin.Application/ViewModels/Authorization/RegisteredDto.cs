using EPortalAdmin.Core.Domain.Entities;
using EPortalAdmin.Core.Security.JWT;

namespace EPortalAdmin.Application.ViewModels.Authorization
{
    public class RegisteredDto
    {
        public AccessToken AccessToken { get; set; }
        public RefreshToken RefreshToken { get; set; }
        public TokenDto TokenDto
        {
            get
            {
                return new(accessToken: AccessToken);
            }
        }

        public RegisteredDto()
        {
            AccessToken = null!;
            RefreshToken = null!;
        }

        public RegisteredDto(AccessToken accessToken, RefreshToken refreshToken)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
        }

    }
}
