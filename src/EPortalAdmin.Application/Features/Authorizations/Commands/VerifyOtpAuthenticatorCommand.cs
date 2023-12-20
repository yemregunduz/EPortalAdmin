using EPortalAdmin.Application.Features.Authorizations.Rules;
using EPortalAdmin.Application.Pipelines.Transaction;
using EPortalAdmin.Application.Repositories;
using EPortalAdmin.Application.Services.AuthenticatorService;
using EPortalAdmin.Application.Wrappers.Results;
using EPortalAdmin.Core.Domain.Entities;
using EPortalAdmin.Core.Domain.Enums;
using EPortalAdmin.Core.Exceptions;
using EPortalAdmin.Domain.Constants;
using MediatR;

namespace EPortalAdmin.Application.Features.Authorizations.Commands
{
    public class VerifyOtpAuthenticatorCommand : IRequest<Result>, ITransactionalRequest
    {
        public string ActivationCode { get; set; }

        public VerifyOtpAuthenticatorCommand()
        {
            ActivationCode = string.Empty;
        }

        public VerifyOtpAuthenticatorCommand(int userId, string activationCode)
        {
            ActivationCode = activationCode;
        }

        public class VerifyOtpAuthenticatorCommandHandler : ApplicationFeatureBase<OtpAuthenticator>, IRequestHandler<VerifyOtpAuthenticatorCommand, Result>
        {
            private readonly AuthorizationBusinessRules _authBusinessRules;
            private readonly IAuthenticatorService _authenticatorService;
            private readonly IUserRepository _userRepository;
            public VerifyOtpAuthenticatorCommandHandler(AuthorizationBusinessRules authBusinessRules,
                IAuthenticatorService authenticatorService, IUserRepository userRepository)
            {
                _authBusinessRules = authBusinessRules;
                _authenticatorService = authenticatorService;
                _userRepository = userRepository;
            }

            public async Task<Result> Handle(VerifyOtpAuthenticatorCommand request, CancellationToken cancellationToken)
            {
                OtpAuthenticator? otpAuthenticator = await Repository.GetAsync(predicate: e => e.UserId == CurrentUserId, cancellationToken: cancellationToken)
                    ?? throw new NotFoundException(Messages.Authorization.OtpAuthenticatorNotFound);

                User? user = await _userRepository.GetAsync(predicate: u => u.Id == CurrentUserId, cancellationToken: cancellationToken)
                    ?? throw new NotFoundException(Messages.Authorization.UserNotFound);

                otpAuthenticator!.IsVerified = true;
                user!.AuthenticatorType = AuthenticatorType.Otp;

                await _authenticatorService.VerifyAuthenticatorCode(user, request.ActivationCode);

                await Repository.UpdateAsync(otpAuthenticator);
                await _userRepository.UpdateAsync(user);

                return new SuccessResult(Messages.Authorization.OtpVerified);
            }
        }
    }
}
