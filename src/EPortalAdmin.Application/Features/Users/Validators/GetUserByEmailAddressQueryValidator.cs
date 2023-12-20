using EPortalAdmin.Application.Features.Users.Queries;
using FluentValidation;

namespace EPortalAdmin.Application.Features.Users.Validators
{
    public class GetUserByEmailAddressQueryValidator : AbstractValidator<GetUserByEmailAddressQuery>
    {
        public GetUserByEmailAddressQueryValidator()
        {
            RuleFor(u => u.EmailAddress)
                .NotEmpty()
                .EmailAddress();
        }
    }
}
