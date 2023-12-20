using EPortalAdmin.Application.ViewModels.Endpoint;
using EPortalAdmin.Application.Wrappers.Results;
using EPortalAdmin.Core.Domain.Entities;
using EPortalAdmin.Core.Domain.Models;
using EPortalAdmin.Core.Persistence.Paging;
using EPortalAdmin.Domain.Constants;
using MediatR;

namespace EPortalAdmin.Application.Features.Endpoints.Queries
{
    public class GetEndpointListQuery : IRequest<DataResult<EndpointListDto>>
    {
        public PagingRequest PagingRequest { get; set; }

        public class GetEndpointListQueryHandler : ApplicationFeatureBase<Endpoint>, IRequestHandler<GetEndpointListQuery, DataResult<EndpointListDto>>
        {
            public async Task<DataResult<EndpointListDto>> Handle(GetEndpointListQuery request, CancellationToken cancellationToken)
            {
                IPaginate<Endpoint> endpoints = await Repository.GetListAsync(index: request.PagingRequest.Page,
                    size: request.PagingRequest.PageSize, cancellationToken: cancellationToken);

                EndpointListDto mappedEndpoints = Mapper.Map<EndpointListDto>(endpoints);

                return new SuccessDataResult<EndpointListDto>(mappedEndpoints, Messages.Endpoint.EndpointListedSuccessfully);
            }
        }
    }
}
