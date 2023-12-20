using EPortalAdmin.Core.Domain.Entities;
using EPortalAdmin.Core.Domain.Enums;
using EPortalAdmin.Core.Security.JWT;

namespace EPortalAdmin.Application.ViewModels.Authorization
{
    public class LoggedInDto
    {
        public AccessToken? AccessToken { get; set; }
        public RefreshToken? RefreshToken { get; set; }
        public AuthenticatorType? RequiredAuthenticatorType { get; set; }
        public LoggedHttpResponse LoggedHttpResponse
        {
            get
            {
                return new(AccessToken, RequiredAuthenticatorType);
            }
        }
    }
    public class LoggedHttpResponse : TokenDto
    {
        public AccessToken? AccessToken { get; set; }
        public AuthenticatorType? RequiredAuthenticatorType { get; set; }
        public LoggedHttpResponse(AccessToken? accessToken, AuthenticatorType? requiredAuthenticatorType) : base(accessToken)
        {
            AccessToken = accessToken;
            RequiredAuthenticatorType = requiredAuthenticatorType;
        }
    }
}
