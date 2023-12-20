using EPortalAdmin.Application.ViewModels.OperationClaim;
using EPortalAdmin.Core.Domain.Entities;
using MediatR;

namespace EPortalAdmin.Application.Features.OperationClaims.Queries
{
    public class GetQueryableOperationClaimsQuery : IRequest<IQueryable<OperationClaimDto>>
    {
        public class GetQueryableOperationClaimsQueryQueryHandler : ApplicationFeatureBase<OperationClaim>, IRequestHandler<GetQueryableOperationClaimsQuery, IQueryable<OperationClaimDto>>
        {
            public async Task<IQueryable<OperationClaimDto>> Handle(GetQueryableOperationClaimsQuery request, CancellationToken cancellationToken)
            {
                IQueryable<OperationClaim> operationClaims = Repository.GetAsQueryable();
                IQueryable<OperationClaimDto> mappingOperationClaims= Mapper.ProjectTo<OperationClaimDto>(operationClaims);
                return await Task.FromResult(mappingOperationClaims);
            }   
        }
    }
}
