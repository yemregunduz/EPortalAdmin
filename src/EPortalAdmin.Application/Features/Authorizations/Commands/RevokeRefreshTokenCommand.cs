using EPortalAdmin.Application.Features.Authorizations.Rules;
using EPortalAdmin.Application.Services.AuthService;
using EPortalAdmin.Application.ViewModels.Authorization;
using EPortalAdmin.Application.Wrappers.Results;
using EPortalAdmin.Core.Domain.Entities;
using EPortalAdmin.Core.Exceptions;
using EPortalAdmin.Domain.Constants;
using MediatR;

namespace EPortalAdmin.Application.Features.Authorizations.Commands
{
    public class RevokeRefreshTokenCommand : IRequest<DataResult<RevokedTokenDto>>
    {
        public string Token { get; set; }
        public string IpAddress { get; set; }

        public class RevokeRefreshTokenCommandHandler : ApplicationFeatureBase<RefreshToken>, IRequestHandler<RevokeRefreshTokenCommand, DataResult<RevokedTokenDto>>
        {
            private readonly IAuthService _authService;
            private readonly AuthorizationBusinessRules _authorizationBusinessRules;

            public RevokeRefreshTokenCommandHandler(IAuthService authService, AuthorizationBusinessRules authorizationBusinessRules)
            {
                _authService = authService;
                _authorizationBusinessRules = authorizationBusinessRules;
            }

            public async Task<DataResult<RevokedTokenDto>> Handle(RevokeRefreshTokenCommand request, CancellationToken cancellationToken)
            {
                RefreshToken? refreshToken = await _authService.GetRefreshTokenByToken(request.Token)
                    ?? throw new NotFoundException(Messages.Authorization.RefreshTokenNotFound);

                await _authorizationBusinessRules.RefreshTokenShouldBeActive(refreshToken!);

                await _authService.RevokeRefreshToken(token: refreshToken!, request.IpAddress, reason: "Revoked without replacement");

                RevokedTokenDto refreshTokenDto = Mapper.Map<RevokedTokenDto>(refreshToken);
                return new SuccessDataResult<RevokedTokenDto>(refreshTokenDto, Messages.Authorization.RefreshTokenRevokedSuccessfully);
            }
        }
    }
}
