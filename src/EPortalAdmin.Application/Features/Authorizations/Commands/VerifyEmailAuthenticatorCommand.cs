using EPortalAdmin.Application.Features.Authorizations.Rules;
using EPortalAdmin.Application.Wrappers.Results;
using EPortalAdmin.Core.Domain.Entities;
using EPortalAdmin.Core.Exceptions;
using EPortalAdmin.Domain.Constants;
using MediatR;

namespace EPortalAdmin.Application.Features.Authorizations.Commands
{
    public class VerifyEmailAuthenticatorCommand : IRequest<Result>
    {
        public string ActivationKey { get; set; }

        public class VerifyEmailAuthenticatorCommandHandler : ApplicationFeatureBase<EmailAuthenticator>, IRequestHandler<VerifyEmailAuthenticatorCommand, Result>
        {
            private readonly AuthorizationBusinessRules _authorizationBusinessRules;
            public VerifyEmailAuthenticatorCommandHandler(AuthorizationBusinessRules authorizationBusinessRules)
            {
                _authorizationBusinessRules = authorizationBusinessRules;
            }

            public async Task<Result> Handle(VerifyEmailAuthenticatorCommand request, CancellationToken cancellationToken)
            {
                EmailAuthenticator? emailAuthenticator = await Repository.GetAsync(
                    predicate: e => e.ActivationKey == request.ActivationKey, cancellationToken: cancellationToken)
                    ?? throw new NotFoundException(Messages.Authorization.EmailAuthenticatorNotFound);
                await _authorizationBusinessRules.EmailAuthenticatorActivationKeyShouldBeExists(emailAuthenticator!);

                emailAuthenticator!.ActivationKey = null;
                emailAuthenticator.IsVerified = true;
                await Repository.UpdateAsync(emailAuthenticator);
                return new SuccessResult(Messages.Authorization.EmailVerified);
            }
        }
    }
}
