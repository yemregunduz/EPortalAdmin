using EPortalAdmin.Application.ViewModels.Endpoint;
using EPortalAdmin.Application.Wrappers.Results;
using EPortalAdmin.Core.Domain.Entities;
using EPortalAdmin.Core.Domain.Models;
using EPortalAdmin.Core.Persistence.Dynamic;
using EPortalAdmin.Core.Persistence.Paging;
using EPortalAdmin.Domain.Constants;
using MediatR;

namespace EPortalAdmin.Application.Features.Endpoints.Queries
{
    public class GetEndpointListByDynamicQuery : IRequest<DataResult<EndpointListDto>>
    {
        public Dynamic Dynamic { get; set; }
        public PagingRequest PagingRequest { get; set; }

        public class GetEndpointListByDynamicQueryHandler : ApplicationFeatureBase<Endpoint>, IRequestHandler<GetEndpointListByDynamicQuery, DataResult<EndpointListDto>>
        {
            public async Task<DataResult<EndpointListDto>> Handle(GetEndpointListByDynamicQuery request, CancellationToken cancellationToken)
            {
                IPaginate<Endpoint> endpoints = await Repository.GetListByDynamicAsync(dynamic: request.Dynamic, index: request.PagingRequest.Page,
                                       size: request.PagingRequest.PageSize, cancellationToken: cancellationToken);

                EndpointListDto mappedEndpoints = Mapper.Map<EndpointListDto>(endpoints);

                return new SuccessDataResult<EndpointListDto>(mappedEndpoints, Messages.Endpoint.EndpointListedSuccessfully);
            }
        }
    }
}
