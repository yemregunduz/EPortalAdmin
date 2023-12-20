using EPortalAdmin.Application.ViewModels.User;
using EPortalAdmin.Application.Wrappers.Results;
using EPortalAdmin.Core.Domain.Entities;
using EPortalAdmin.Core.Domain.Enums;
using EPortalAdmin.Core.Exceptions;
using EPortalAdmin.Domain.Constants;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EPortalAdmin.Application.Features.Users.Queries
{
    public class GetUserByEmailAddressQuery : IRequest<DataResult<UserDto>>
    {
        public string EmailAddress { get; set; }

        public class GetUserByEmailAdressQueryHandler : ApplicationFeatureBase<User>, IRequestHandler<GetUserByEmailAddressQuery, DataResult<UserDto>>
        {
            public async Task<DataResult<UserDto>> Handle(GetUserByEmailAddressQuery request, CancellationToken cancellationToken)
            {
                User? user = await Repository.GetAsync(
                    include: m => m.Include(u => u.UserOperationClaims).ThenInclude(uoc => uoc.OperationClaim),
                    predicate: u => u.Email == request.EmailAddress,
                    cancellationToken: cancellationToken)
                    ?? throw new NotFoundException(Messages.User.UserNotFound, ExceptionCode.UserNotFound);

                UserDto mappedUser = Mapper.Map<UserDto>(user);

                return new SuccessDataResult<UserDto>(mappedUser, Messages.User.GetUserSuccessfully);
            }
        }
    }
}
