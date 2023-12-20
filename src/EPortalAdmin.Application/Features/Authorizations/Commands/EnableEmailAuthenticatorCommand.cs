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

        public class EnableEmailAuthenticatorCommandHandler(IUserRepository userRepository, IMailService mailService, IAuthenticatorService authenticatorService,
            AuthorizationBusinessRules authorizationBusinessRules) : ApplicationFeatureBase<EmailAuthenticator>, IRequestHandler<EnableEmailAuthenticatorCommand>
        {
            public async Task Handle(EnableEmailAuthenticatorCommand request, CancellationToken cancellationToken)
            {
                User? user = await userRepository.GetAsync(predicate: u => u.Id == CurrentUserId, cancellationToken: cancellationToken) ??
                    throw new NotFoundException(Messages.User.UserNotFound, ExceptionCode.UserNotFound);

                await authorizationBusinessRules.UserShouldNotBeHaveAuthenticator(user);

                user!.AuthenticatorType = AuthenticatorType.Email;

                await userRepository.UpdateAsync(user, cancellationToken);

                EmailAuthenticator emailAuthenticator = await authenticatorService.CreateEmailAuthenticator(user);
                EmailAuthenticator addedEmailAuthenticator = await Repository.AddAsync(emailAuthenticator, cancellationToken);

                var toEmailList = new List<MailboxAddress> { new(name: $"{user.FirstName} {user.LastName}", user.Email) };

                mailService.SendMail(
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
