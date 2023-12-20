using EPortalAdmin.Application.Repositories;
using EPortalAdmin.Core.Domain.Entities;
using EPortalAdmin.Core.Domain.Enums;
using EPortalAdmin.Core.Exceptions;
using EPortalAdmin.Domain.Constants;

namespace EPortalAdmin.Application.Features.Authorizations.Rules
{
    public class AuthorizationBusinessRules(IUserRepository userRepository)
    {
        public async Task EmailCanNotBeDuplicatedWhenRegistered(string email)
        {
            User? user = await userRepository.GetAsync(u => u.Email == email);
            if (user != null)
                throw new BusinessException(Messages.Authorization.EmailAlreadyExist, ExceptionCode.EmailAlreadyExist);
        }
        public void PasswordsCannotBeSameWhenPasswordChanged(string currentPassword, string newPassword)
        {
            if (currentPassword == newPassword)
                throw new BusinessException(Messages.Authorization.SamePasswordError, ExceptionCode.SamePassword);
        }
        public Task UserShouldNotBeHaveAuthenticator(User user)
        {
            if (user.AuthenticatorType != AuthenticatorType.None)
                throw new BusinessException(Messages.Authorization.UserHaveAlreadyAAuthenticator, ExceptionCode.UserAlreadyHasAuthenticator);
            return Task.CompletedTask;
        }

        public Task OtpAuthenticatorThatVerifiedShouldNotBeExists(OtpAuthenticator? otpAuthenticator)
        {
            if (otpAuthenticator is not null && otpAuthenticator.IsVerified)
                throw new BusinessException(Messages.Authorization.AlreadyVerifiedOtpAuthenticatorIsExists);
            return Task.CompletedTask;
        }

        public Task RefreshTokenShouldBeActive(RefreshToken refreshToken)
        {
            if (refreshToken.Revoked != null && DateTime.UtcNow >= refreshToken.Expires)
                throw new BusinessException(Messages.Authorization.InvalidRefreshToken, ExceptionCode.InvalidRefreshToken);
            return Task.CompletedTask;
        }

        public Task EmailAuthenticatorActivationKeyShouldBeExists(EmailAuthenticator emailAuthenticator)
        {
            if (emailAuthenticator.ActivationKey is null)
                throw new BusinessException(Messages.Authorization.EmailActivationKeyDoesntExists, ExceptionCode.EmailActivationKeyNotFound);
            return Task.CompletedTask;
        }
    }
}
