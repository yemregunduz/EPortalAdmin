using EPortalAdmin.Application.Features.Authorizations.Rules;
using EPortalAdmin.Application.Repositories;
using EPortalAdmin.Application.Services.AuthenticatorService;
using EPortalAdmin.Core.Domain.Entities;
using EPortalAdmin.Core.Domain.Enums;
using EPortalAdmin.Core.Exceptions;
using EPortalAdmin.Core.Mailing;
using EPortalAdmin.Domain.Constants;
using MediatR;
using MimeKit;
using System.Web;

namespace EPortalAdmin.Application.Features.Authorizations.Commands
{
    public class EnableEmailAuthenticatorCommand : IRequest
    {
        public string VerifyEmailUrlPrefix { get; set; }

        public class EnableEmailAuthenticatorCommandHandler : ApplicationFeatureBase<EmailAuthenticator>, IRequestHandler<EnableEmailAuthenticatorCommand>
        {
            private readonly IUserRepository _userRepository;
            private readonly IMailService _mailService;
            private readonly IAuthenticatorService _authenticatorService;
            private readonly AuthorizationBusinessRules _authorizationBusinessRules;

            public EnableEmailAuthenticatorCommandHandler(IUserRepository userRepository, IMailService mailService, IAuthenticatorService authenticatorService,
                AuthorizationBusinessRules authorizationBusinessRules)
            {
                _userRepository = userRepository;
                _mailService = mailService;
                _authenticatorService = authenticatorService;
                _authorizationBusinessRules = authorizationBusinessRules;
            }

            public async Task Handle(EnableEmailAuthenticatorCommand request, CancellationToken cancellationToken)
            {
                User? user = await _userRepository.GetAsync(predicate: u => u.Id == CurrentUserId, cancellationToken: cancellationToken) ??
                    throw new NotFoundException(Messages.User.UserNotFound);

                await _authorizationBusinessRules.UserShouldNotBeHaveAuthenticator(user);

                user!.AuthenticatorType = AuthenticatorType.Email;

                await _userRepository.UpdateAsync(user);

                EmailAuthenticator emailAuthenticator = await _authenticatorService.CreateEmailAuthenticator(user);
                EmailAuthenticator addedEmailAuthenticator = await Repository.AddAsync(emailAuthenticator);

                var toEmailList = new List<MailboxAddress> { new(name: $"{user.FirstName} {user.LastName}", user.Email) };

                _mailService.SendMail(
                    new Mail
                    {
                        ToList = toEmailList,
                        Subject = "Emailinizi onaylayın. - Mr.Peinir",
                        TextBody =
                            $"Linke tıklayarak mailinizi onaylayabilirsiniz: {request.VerifyEmailUrlPrefix}?ActivationKey={HttpUtility.UrlEncode(addedEmailAuthenticator.ActivationKey)}"
                    }
                );

            }
        }
    }
}
