using EPortalAdmin.Application.Features.Authorizations.Rules;
using EPortalAdmin.Application.Repositories;
using EPortalAdmin.Application.Services.AuthenticatorService;
using EPortalAdmin.Application.ViewModels.OtpAuthenticator;
using EPortalAdmin.Application.Wrappers.Results;
using EPortalAdmin.Core.Domain.Entities;
using EPortalAdmin.Core.Domain.Enums;
using EPortalAdmin.Core.Exceptions;
using EPortalAdmin.Domain.Constants;
using MediatR;

namespace EPortalAdmin.Application.Features.Authorizations.Commands
{
    public class EnableOtpAuthenticatorCommand : IRequest<DataResult<OtpAuthenticatorDto>>
    {
        public class EnableOtpAuthenticatorCommandHandler(AuthorizationBusinessRules authBusinessRules, IAuthenticatorService authenticatorService, IUserRepository userRepository) 
            : ApplicationFeatureBase<OtpAuthenticator>, IRequestHandler<EnableOtpAuthenticatorCommand, DataResult<OtpAuthenticatorDto>>
        {
            public async Task<DataResult<OtpAuthenticatorDto>> Handle(EnableOtpAuthenticatorCommand request, CancellationToken cancellationToken)
            {
                User user = await userRepository.GetAsync(predicate: u => u.Id == CurrentUserId, cancellationToken: cancellationToken)
                    ?? throw new NotFoundException(Messages.Authorization.UserNotFound, ExceptionCode.UserNotFound);

                await authBusinessRules.UserShouldNotBeHaveAuthenticator(user!);
                OtpAuthenticator? doesExistOtpAuthenticator = await Repository.GetAsync(predicate: o => o.UserId == CurrentUserId, cancellationToken: cancellationToken);

                await authBusinessRules.OtpAuthenticatorThatVerifiedShouldNotBeExists(doesExistOtpAuthenticator);
                if (doesExistOtpAuthenticator is not null)
                    await Repository.DeleteAsync(doesExistOtpAuthenticator);

                OtpAuthenticator newOtpAuthenticator = await authenticatorService.CreateOtpAuthenticator(user!);
                OtpAuthenticator addedOtpAuthenticator = await Repository.AddAsync(newOtpAuthenticator, cancellationToken);

                OtpAuthenticatorDto otpAuthenticatorDto =
                    new() { SecretKey = await authenticatorService.ConvertSecretKeyToString(addedOtpAuthenticator.SecretKey) };

                return new SuccessDataResult<OtpAuthenticatorDto>(otpAuthenticatorDto);
            }
        }
    }
}
