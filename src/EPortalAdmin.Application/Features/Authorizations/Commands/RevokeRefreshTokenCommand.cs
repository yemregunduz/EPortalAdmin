using EPortalAdmin.Application.Features.Authorizations.Rules;
using EPortalAdmin.Application.Services.AuthService;
using EPortalAdmin.Application.ViewModels.Authorization;
using EPortalAdmin.Application.Wrappers.Results;
using EPortalAdmin.Core.Domain.Entities;
using EPortalAdmin.Core.Domain.Enums;
using EPortalAdmin.Core.Exceptions;
using EPortalAdmin.Domain.Constants;
using MediatR;

namespace EPortalAdmin.Application.Features.Authorizations.Commands
{
    public class RevokeRefreshTokenCommand : IRequest<DataResult<RevokedTokenDto>>
    {
        public string Token { get; set; }
        public string IpAddress { get; set; }

        public class RevokeRefreshTokenCommandHandler(IAuthService authService, AuthorizationBusinessRules authorizationBusinessRules) : 
            ApplicationFeatureBase<RefreshToken>, IRequestHandler<RevokeRefreshTokenCommand, DataResult<RevokedTokenDto>>
        {
            public async Task<DataResult<RevokedTokenDto>> Handle(RevokeRefreshTokenCommand request, CancellationToken cancellationToken)
            {
                RefreshToken? refreshToken = await authService.GetRefreshTokenByToken(request.Token)
                    ?? throw new NotFoundException(Messages.Authorization.RefreshTokenNotFound,ExceptionCode.RefreshTokenNotFound);

                await authorizationBusinessRules.RefreshTokenShouldBeActive(refreshToken!);

                await authService.RevokeRefreshToken(token: refreshToken!, request.IpAddress, reason: "Revoked without replacement");

                RevokedTokenDto refreshTokenDto = Mapper.Map<RevokedTokenDto>(refreshToken);
                return new SuccessDataResult<RevokedTokenDto>(refreshTokenDto, Messages.Authorization.RefreshTokenRevokedSuccessfully);
            }
        }
    }
}
