using EPortalAdmin.Core.Domain.Entities;
using EPortalAdmin.Core.Security.JWT;

namespace EPortalAdmin.Application.ViewModels.Authorization
{
    public class RefreshedTokenDto
    {
        public AccessToken AccessToken { get; set; }
        public RefreshToken RefreshToken { get; set; }
        public TokenDto TokenDto
        {
            get
            {
                return new(AccessToken);
            }
        }
        public RefreshedTokenDto(AccessToken accessToken, RefreshToken refreshToken)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
        }
    }
}
