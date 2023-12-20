using EPortalAdmin.Application.Features.Authorizations.Rules;
using EPortalAdmin.Application.Services.AuthService;
using EPortalAdmin.Application.ViewModels.Authorization;
using EPortalAdmin.Application.Wrappers.Results;
using EPortalAdmin.Core.Domain.Entities;
using EPortalAdmin.Core.Domain.Enums;
using EPortalAdmin.Core.Exceptions;
using EPortalAdmin.Core.Security.JWT;
using EPortalAdmin.Domain.Constants;
using MediatR;

namespace EPortalAdmin.Application.Features.Authorizations.Commands
{
    public class RefreshTokenCommand : IRequest<DataResult<RefreshedTokenDto>>
    {
        public string RefreshToken { get; set; }
        public string IpAddress { get; set; }

        public class RefreshTokenCommandHandler(IAuthService authService, AuthorizationBusinessRules authorizationBusinessRules) 
            : ApplicationFeatureBase<User>, IRequestHandler<RefreshTokenCommand, DataResult<RefreshedTokenDto>>
        {
            private readonly IAuthService _authService = authService;
            private readonly AuthorizationBusinessRules _authorizationBusinessRules = authorizationBusinessRules;

            public async Task<DataResult<RefreshedTokenDto>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
            {
                RefreshToken refreshToken = await _authService.GetRefreshTokenByToken(request.RefreshToken)
                    ?? throw new AuthorizationException(Messages.Authorization.RefreshTokenNotFound,ExceptionCode.RefreshTokenNotFound);

                if (refreshToken!.Revoked != null)
                {
                    await _authService.RevokeDescendantRefreshTokens(
                        refreshToken,
                        request.IpAddress,
                        reason: $"Attempted reuse of revoked ancestor token: {refreshToken.Token}");
                }

                await _authorizationBusinessRules.RefreshTokenShouldBeActive(refreshToken);

                User user = await Repository.GetAsync(predicate: u => u.Id == refreshToken.UserId, cancellationToken: cancellationToken)
                    ?? throw new NotFoundException(Messages.Authorization.UserNotFound, ExceptionCode.UserNotFound);

                RefreshToken newRefreshToken = await _authService.RotateRefreshToken(user: user!, refreshToken, request.IpAddress);

                AccessToken createdAccessToken = await _authService.CreateAccessToken(user);
                RefreshToken addedRefreshToken = await _authService.AddRefreshToken(newRefreshToken);

                await _authService.DeleteOldRefreshTokens(refreshToken.UserId);

                RefreshedTokenDto refreshedTokenDto = new(accessToken: createdAccessToken, refreshToken: addedRefreshToken);

                return new SuccessDataResult<RefreshedTokenDto>(refreshedTokenDto, Messages.Authorization.TokenRefreshedSuccessfully);
            }
        }
    }
}
