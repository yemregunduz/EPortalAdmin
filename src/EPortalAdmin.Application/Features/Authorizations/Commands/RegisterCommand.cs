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

        public class RegisterCommandHandler(IAuthService authService, AuthorizationBusinessRules authBusinessRules) 
            : ApplicationFeatureBase<User>, IRequestHandler<RegisterCommand, DataResult<RegisteredDto>>
        {
            public async Task<DataResult<RegisteredDto>> Handle(RegisterCommand request, CancellationToken cancellationToken)
            {
                await authBusinessRules.EmailCanNotBeDuplicatedWhenRegistered(request.UserForRegisterDto.Email);

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

                User createdUser = await Repository.AddAsync(user,cancellationToken);

                AccessToken createdAccessToken = await authService.CreateAccessToken(createdUser);
                RefreshToken createdRefreshToken = await authService.CreateRefreshToken(createdUser, request.IpAddress);
                RefreshToken addedRefreshToken = await authService.AddRefreshToken(createdRefreshToken);

                RegisteredDto registeredDto = new(accessToken: createdAccessToken, refreshToken: addedRefreshToken);

                return new SuccessDataResult<RegisteredDto>(registeredDto, Messages.Authorization.UserCreatedSuccessfully);
            }
        }
    }
}