using EPortalAdmin.Application.ViewModels.User;
using EPortalAdmin.Application.Wrappers.Results;
using EPortalAdmin.Core.Domain.Entities;
using EPortalAdmin.Core.Domain.Models;
using EPortalAdmin.Core.Persistence.Dynamic;
using EPortalAdmin.Core.Persistence.Paging;
using EPortalAdmin.Domain.Constants;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EPortalAdmin.Application.Features.Users.Queries
{
    public class GetUserListByDynamicQuery : IRequest<DataResult<UserListDto>>
    {
        public Dynamic Dynamic { get; set; }
        public PagingRequest PagingRequest { get; set; }

        public class GetUserListByDynamicQueryHandler : ApplicationFeatureBase<User>, IRequestHandler<GetUserListByDynamicQuery, DataResult<UserListDto>>
        {
            public async Task<DataResult<UserListDto>> Handle(GetUserListByDynamicQuery request, CancellationToken cancellationToken)
            {
                IPaginate<User> users = await Repository.GetListByDynamicAsync(
                    dynamic: request.Dynamic,
                    include: m => m.Include(u => u.UserOperationClaims).ThenInclude(uoc => uoc.OperationClaim),
                    index: request.PagingRequest.Page,
                    size: request.PagingRequest.PageSize
                    );

                UserListDto mappedUsers = Mapper.Map<UserListDto>(users);

                return new SuccessDataResult<UserListDto>(mappedUsers, Messages.User.UserListedSuccessfully);
            }
        }
    }
}
