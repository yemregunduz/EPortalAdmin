using EPortalAdmin.Application.Features.Authorizations.Rules;
using EPortalAdmin.Application.Services.AuthService;
using EPortalAdmin.Application.ViewModels.Authorization;
using EPortalAdmin.Application.Wrappers.Results;
using EPortalAdmin.Core.Domain.Dtos;
using EPortalAdmin.Core.Domain.Entities;
using EPortalAdmin.Core.Security.JWT;
using EPortalAdmin.Core.Utilities.Helpers;
using EPortalAdmin.Domain.Constants;
using MediatR;

namespace EPortalAdmin.Application.Features.Authorizations.Commands
{
    public class RegisterCommand : IRequest<DataResult<RegisteredDto>>
    {
        public UserForRegisterDto UserForRegisterDto { get; set; }
        public string IpAddress { get; set; }

        public class RegisterCommandHandler : ApplicationFeatureBase<User>, IRequestHandler<RegisterCommand, DataResult<RegisteredDto>>
        {
            private readonly IAuthService _authService;
            private readonly AuthorizationBusinessRules _authBusinessRules;

            public RegisterCommandHandler(IAuthService authService, AuthorizationBusinessRules authBusinessRules)
            {
                _authService = authService;
                _authBusinessRules = authBusinessRules;
            }

            public async Task<DataResult<RegisteredDto>> Handle(RegisterCommand request, CancellationToken cancellationToken)
            {
                await _authBusinessRules.EmailCanNotBeDuplicatedWhenRegistered(request.UserForRegisterDto.Email);

                HashingHelper.CreatePasswordHash(request.UserForRegisterDto.Password, out byte[] passwordHash, out byte[] passwordSalt);

                User user = new()
                {
                    Email = request.UserForRegisterDto.Email,
                    FirstName = request.UserForRegisterDto.FirstName,
                    LastName = request.UserForRegisterDto.LastName,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                    Status = true,
                };

                User createdUser = await Repository.AddAsync(user);

                AccessToken createdAccessToken = await _authService.CreateAccessToken(createdUser);
                RefreshToken createdRefreshToken = await _authService.CreateRefreshToken(createdUser, request.IpAddress);
                RefreshToken addedRefreshToken = await _authService.AddRefreshToken(createdRefreshToken);

                RegisteredDto registeredDto = new(accessToken: createdAccessToken, refreshToken: addedRefreshToken);

                return new SuccessDataResult<RegisteredDto>(registeredDto, Messages.Authorization.UserCreatedSuccessfully);
            }
        }
    }
}