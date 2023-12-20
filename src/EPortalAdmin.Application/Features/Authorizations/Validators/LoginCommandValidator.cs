using EPortalAdmin.Application.Features.Authorizations.Commands;
using FluentValidation;

namespace EPortalAdmin.Application.Features.Authorizations.Validators
{
    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            //RuleFor(u => u.UserForLoginDto.Email)
            //    .NotNull()
            //    .NotEmpty()
            //    .EmailAddress();

            //RuleFor(u => u.UserForLoginDto.Password)
            //    .NotNull()
            //    .NotEmpty()
            //    .Matches(RegexHelper.PasswordRegex())
            //    .WithMessage(Messages.Authorization.InvalidPasswordRegex);
        }
    }
}
