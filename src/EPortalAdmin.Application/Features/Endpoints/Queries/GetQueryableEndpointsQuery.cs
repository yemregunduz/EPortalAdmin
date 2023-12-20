using EPortalAdmin.Application.ViewModels.Endpoint;
using EPortalAdmin.Core.Domain.Entities;
using MediatR;

namespace EPortalAdmin.Application.Features.Endpoints.Queries
{
    public class GetQueryableEndpointsQuery : IRequest<IQueryable<EndpointDto>>
    {
        public class GetQueryableEndpointsQueryQueryHandler : ApplicationFeatureBase<Endpoint>, IRequestHandler<GetQueryableEndpointsQuery, IQueryable<EndpointDto>>
        {
            public async Task<IQueryable<EndpointDto>> Handle(GetQueryableEndpointsQuery request, CancellationToken cancellationToken)
            {
                IQueryable<Endpoint> endpoints = Repository.GetAsQueryable();
                IQueryable<EndpointDto> mappingEndpoints = Mapper.ProjectTo<EndpointDto>(endpoints);
                return await Task.FromResult(mappingEndpoints);
            }
        }
    }
}
