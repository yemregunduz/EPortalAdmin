using EPortalAdmin.Application.Features.Authorizations.Commands;
using FluentValidation;

namespace EPortalAdmin.Application.Features.Authorizations.Validators
{
    public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator()
        {
            //RuleFor(u => u.UserForRegisterDto.Email)
            //    .NotEmpty()
            //    .EmailAddress();
            //RuleFor(u => u.UserForRegisterDto.Password)
            //    .MinimumLength(8);
        }
    }
}
