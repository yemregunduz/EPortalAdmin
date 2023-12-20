using EPortalAdmin.Application.Features.Authorizations.Rules;
using EPortalAdmin.Application.Repositories;
using EPortalAdmin.Application.Services.AuthenticatorService;
using EPortalAdmin.Application.ViewModels.OtpAuthenticator;
using EPortalAdmin.Application.Wrappers.Results;
using EPortalAdmin.Core.Domain.Entities;
using EPortalAdmin.Core.Exceptions;
using EPortalAdmin.Domain.Constants;
using MediatR;

namespace EPortalAdmin.Application.Features.Authorizations.Commands
{
    public class EnableOtpAuthenticatorCommand : IRequest<DataResult<OtpAuthenticatorDto>>
    {
        public class EnableOtpAuthenticatorCommandHandler : ApplicationFeatureBase<OtpAuthenticator>, IRequestHandler<EnableOtpAuthenticatorCommand, DataResult<OtpAuthenticatorDto>>
        {
            private readonly AuthorizationBusinessRules _authBusinessRules;
            private readonly IAuthenticatorService _authenticatorService;
            private readonly IUserRepository _userRepository;

            public EnableOtpAuthenticatorCommandHandler(AuthorizationBusinessRules authBusinessRules, IAuthenticatorService authenticatorService, IUserRepository userRepository)
            {
                _authBusinessRules = authBusinessRules;
                _authenticatorService = authenticatorService;
                _userRepository = userRepository;
            }

            public async Task<DataResult<OtpAuthenticatorDto>> Handle(EnableOtpAuthenticatorCommand request, CancellationToken cancellationToken)
            {
                User user = await _userRepository.GetAsync(predicate: u => u.Id == CurrentUserId, cancellationToken: cancellationToken)
                    ?? throw new NotFoundException(Messages.Authorization.UserNotFound);

                await _authBusinessRules.UserShouldNotBeHaveAuthenticator(user!);
                OtpAuthenticator? doesExistOtpAuthenticator = await Repository.GetAsync(predicate: o => o.UserId == CurrentUserId, cancellationToken: cancellationToken);

                await _authBusinessRules.OtpAuthenticatorThatVerifiedShouldNotBeExists(doesExistOtpAuthenticator);
                if (doesExistOtpAuthenticator is not null)
                    await Repository.DeleteAsync(doesExistOtpAuthenticator);

                OtpAuthenticator newOtpAuthenticator = await _authenticatorService.CreateOtpAuthenticator(user!);
                OtpAuthenticator addedOtpAuthenticator = await Repository.AddAsync(newOtpAuthenticator);

                OtpAuthenticatorDto otpAuthenticatorDto =
                    new() { SecretKey = await _authenticatorService.ConvertSecretKeyToString(addedOtpAuthenticator.SecretKey) };

                return new SuccessDataResult<OtpAuthenticatorDto>(otpAuthenticatorDto);
            }
        }
    }
}
