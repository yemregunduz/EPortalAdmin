using EPortalAdmin.Application.Features.UserOperationClaims.Commands;
using FluentValidation;

namespace EPortalAdmin.Application.Features.UserOperationClaims.Validators
{
    public class CreateUserOperationClaimCommandValidator : AbstractValidator<CreateUserOperationClaimCommand>
    {
        public CreateUserOperationClaimCommandValidator()
        {
            RuleFor(uoc => uoc.UserId)
                .NotNull()
                .NotEmpty();
        }
    }
}
