using EPortalAdmin.Application.Features.OperationClaims.Commands;
using FluentValidation;

namespace EPortalAdmin.Application.Features.OperationClaims.Validators
{
    public class CreateOperationClaimCommandValidator : AbstractValidator<CreateOperationClaimCommand>
    {
        public CreateOperationClaimCommandValidator()
        {
            RuleFor(o => o.Name)
                .NotEmpty()
                .MinimumLength(3);
        }
    }
}
