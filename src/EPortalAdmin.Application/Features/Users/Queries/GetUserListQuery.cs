using EPortalAdmin.Application.Pipelines.Authorization;
using EPortalAdmin.Application.ViewModels.User;
using EPortalAdmin.Application.Wrappers.Results;
using EPortalAdmin.Core.Domain.Entities;
using EPortalAdmin.Core.Domain.Models;
using EPortalAdmin.Core.Persistence.Paging;
using EPortalAdmin.Domain.Constants;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EPortalAdmin.Application.Features.Users.Queries
{
    public class GetUserListQuery : IRequest<DataResult<UserListDto>>, ISecuredRequest
    {
        public PagingRequest PagingRequest { get; set; }

        public class GetUserListQueryHandler : ApplicationFeatureBase<User>, IRequestHandler<GetUserListQuery, DataResult<UserListDto>>
        {
            public async Task<DataResult<UserListDto>> Handle(GetUserListQuery request, CancellationToken cancellationToken)
            {
                IPaginate<User> users = await Repository.GetListAsync(
                    include: m => m.Include(u => u.UserOperationClaims).ThenInclude(uoc => uoc.OperationClaim),
                    index: request.PagingRequest.Page,
                    size: request.PagingRequest.PageSize);

                UserListDto mappedUsers = Mapper.Map<UserListDto>(users);

                return new SuccessDataResult<UserListDto>(mappedUsers, Messages.User.UserListedSuccessfully);
            }
        }
    }
}
