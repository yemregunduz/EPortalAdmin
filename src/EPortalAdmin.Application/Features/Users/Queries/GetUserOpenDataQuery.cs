using EPortalAdmin.Application.ViewModels.User;
using EPortalAdmin.Core.Domain.Entities;
using MediatR;

namespace EPortalAdmin.Application.Features.Users.Queries
{
    public class GetQueryableUsersQuery : IRequest<IQueryable<UserDto>>
    {
        public class GetQueryableUsersQueryQueryHandler : ApplicationFeatureBase<User>, IRequestHandler<GetQueryableUsersQuery, IQueryable<UserDto>>
        {

            Task<IQueryable<UserDto>> IRequestHandler<GetQueryableUsersQuery, IQueryable<UserDto>>.Handle(GetQueryableUsersQuery request, CancellationToken cancellationToken)
            {
                IQueryable<User> users = Repository.GetAsQueryable();
                IQueryable<UserDto> mappingUsers= Mapper.ProjectTo<UserDto>(users);
                return Task.FromResult(mappingUsers);
            }
        }
    }
}
