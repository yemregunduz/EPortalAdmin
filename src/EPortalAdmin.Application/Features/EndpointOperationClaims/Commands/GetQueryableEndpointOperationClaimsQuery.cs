using EPortalAdmin.Application.ViewModels.EndpointOperationClaim;
using EPortalAdmin.Core.Domain.Entities;
using MediatR;

namespace EPortalAdmin.Application.Features.EndpointOperationClaims.Commands
{
    public class GetQueryableEndpointOperationClaimsQuery : IRequest<IQueryable<EndpointOperationClaimDto>>
    {
        public class GetQueryableEndpointOperationClaimsQueryQueryHandler : ApplicationFeatureBase<EndpointOperationClaim>,
            IRequestHandler<GetQueryableEndpointOperationClaimsQuery, IQueryable<EndpointOperationClaimDto>>
        {
            public Task<IQueryable<EndpointOperationClaimDto>> Handle(GetQueryableEndpointOperationClaimsQuery request, CancellationToken cancellationToken)
            {
                IQueryable<EndpointOperationClaim> endpointOperationClaims = Repository.GetAsQueryable();
                IQueryable<EndpointOperationClaimDto> mappingEndpointOperationClaims= Mapper.ProjectTo<EndpointOperationClaimDto>(endpointOperationClaims);
                return Task.FromResult(mappingEndpointOperationClaims);
            }
        }
    }
}
