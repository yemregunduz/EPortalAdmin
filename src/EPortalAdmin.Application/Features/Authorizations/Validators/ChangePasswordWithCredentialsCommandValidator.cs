using EPortalAdmin.Application.Features.Authorizations.Commands;
using EPortalAdmin.Domain.Constants;
using FluentValidation;
using Yeg.Utilities.Helpers;

namespace EPortalAdmin.Application.Features.Authorizations.Validators
{
    public class ChangePasswordWithCredentialsCommandValidator : AbstractValidator<ChangePasswordWithCredentialsCommand>
    {
        public ChangePasswordWithCredentialsCommandValidator()
        {
            RuleFor(u => u.UserForChangePasswordDto.Email)
                .NotNull()
                .NotEmpty()
                .EmailAddress();

            RuleFor(u => u.UserForChangePasswordDto.NewPassword)
                .NotNull()
                .NotEmpty()
                .Matches(RegexHelper.PasswordRegex())
                .WithMessage(Messages.Authorization.InvalidPasswordRegex);
        }
    }
}
