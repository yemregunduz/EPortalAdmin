using EPortalAdmin.Application.ViewModels.UserOperationClaim;
using EPortalAdmin.Core.Domain.Entities;
using MediatR;

namespace EPortalAdmin.Application.Features.UserOperationClaims.Queries
{
    public class GetQueryableUserOperationClaimsQuery : IRequest<IQueryable<UserOperationClaimDto>>
    {
        public class GetQueryableUserOperationsClaimsQueryQueryHandler : ApplicationFeatureBase<UserOperationClaim>,
            IRequestHandler<GetQueryableUserOperationClaimsQuery, IQueryable<UserOperationClaimDto>>
        {
            public async Task<IQueryable<UserOperationClaimDto>> Handle(GetQueryableUserOperationClaimsQuery request, CancellationToken cancellationToken)
            {
                IQueryable<UserOperationClaim> userOperationClaims = Repository.GetAsQueryable();
                IQueryable<UserOperationClaimDto> mappingUserOperationClaims = Mapper.ProjectTo<UserOperationClaimDto>(userOperationClaims);
                return await Task.FromResult(mappingUserOperationClaims);
            }

        }
    }
}
