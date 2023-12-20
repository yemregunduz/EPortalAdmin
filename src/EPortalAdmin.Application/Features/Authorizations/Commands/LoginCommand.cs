using EPortalAdmin.Application.Services.AuthenticatorService;
using EPortalAdmin.Application.Services.AuthService;
using EPortalAdmin.Application.ViewModels.Authorization;
using EPortalAdmin.Application.Wrappers.Results;
using EPortalAdmin.Core.Domain.Dtos;
using EPortalAdmin.Core.Domain.Entities;
using EPortalAdmin.Core.Domain.Enums;
using EPortalAdmin.Core.Exceptions;
using EPortalAdmin.Core.Security.JWT;
using EPortalAdmin.Core.Utilities.Helpers;
using EPortalAdmin.Domain.Constants;
using MediatR;

namespace EPortalAdmin.Application.Features.Authorizations.Commands
{
    public class LoginCommand : IRequest<DataResult<LoggedInDto>>
    {
        public UserForLoginDto UserForLoginDto { get; set; }
        public string IpAddress { get; set; }

        public class LoginCommandHandler : ApplicationFeatureBase<User>, IRequestHandler<LoginCommand, DataResult<LoggedInDto>>
        {
            private readonly IAuthService _authService;
            private readonly IAuthenticatorService _authenticatorService;
            public LoginCommandHandler(IAuthService authService, IAuthenticatorService authenticatorService)
            {
                _authService = authService;
                _authenticatorService = authenticatorService;
            }

            public async Task<DataResult<LoggedInDto>> Handle(LoginCommand request, CancellationToken cancellationToken)
            {
                User? user = await Repository.GetAsync(predicate: u => u.Email == request.UserForLoginDto.Email, cancellationToken: cancellationToken)
                    ?? throw new NotFoundException(Messages.Authorization.UserNotFound);

                if (!HashingHelper.VerifyPasswordHash(request.UserForLoginDto.Password, user.PasswordHash, user.PasswordSalt))
                    throw new AuthorizationException(Messages.Authorization.InvalidCredentials);


                LoggedInDto loggedInDto = new();
                if (user.AuthenticatorType is not AuthenticatorType.None)
                {
                    if (request.UserForLoginDto.AuthenticatorCode is null)
                    {
                        await _authenticatorService.SendAuthenticatorCode(user);
                        loggedInDto.RequiredAuthenticatorType = user.AuthenticatorType;
                        return new SuccessDataResult<LoggedInDto>(loggedInDto);
                    }

                    await _authenticatorService.VerifyAuthenticatorCode(user, request.UserForLoginDto.AuthenticatorCode);
                }
                AccessToken createdAccessToken = await _authService.CreateAccessToken(user);
                RefreshToken createdRefreshToken = await _authService.CreateRefreshToken(user, request.IpAddress);
                RefreshToken addedRefreshToken = await _authService.AddRefreshToken(createdRefreshToken);

                loggedInDto.AccessToken = createdAccessToken;
                loggedInDto.RefreshToken = createdRefreshToken;
                await _authService.DeleteOldRefreshTokens(user.Id);

                return new SuccessDataResult<LoggedInDto>(loggedInDto, Messages.Authorization.UserLoggedInSuccessfully);
            }
        }
    }
}
