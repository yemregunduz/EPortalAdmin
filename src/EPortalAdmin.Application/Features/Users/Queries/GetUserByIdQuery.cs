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
    public class GetUserByIdQuery : IRequest<DataResult<UserDto>>
    {
        public int Id { get; set; }

        public class GetUserByIdQueryHandler : ApplicationFeatureBase<User>, IRequestHandler<GetUserByIdQuery, DataResult<UserDto>>
        {
            public async Task<DataResult<UserDto>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
            {
                User? user = await Repository.GetAsync(
                    include: m => m.Include(u => u.UserOperationClaims).ThenInclude(uoc => uoc.OperationClaim),
                    predicate: u => u.Id == request.Id,
                    cancellationToken: cancellationToken)
                    ?? throw new NotFoundException(Messages.User.UserNotFound, ExceptionCode.UserNotFound);

                UserDto userDto = Mapper.Map<UserDto>(user);

                return new SuccessDataResult<UserDto>(userDto, Messages.User.GetUserSuccessfully);
            }
        }
    }
}
