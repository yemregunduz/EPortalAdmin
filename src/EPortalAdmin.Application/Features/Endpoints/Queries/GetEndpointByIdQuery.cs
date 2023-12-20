using EPortalAdmin.Application.ViewModels.Endpoint;
using EPortalAdmin.Application.Wrappers.Results;
using EPortalAdmin.Core.Domain.Entities;
using EPortalAdmin.Core.Domain.Enums;
using EPortalAdmin.Core.Exceptions;
using EPortalAdmin.Domain.Constants;
using MediatR;

namespace EPortalAdmin.Application.Features.Endpoints.Queries
{
    public class GetEndpointByIdQuery : IRequest<DataResult<EndpointDto>>
    {
        public int Id { get; set; }

        public class GetEndpointByIdQueryHandler : ApplicationFeatureBase<Endpoint>, IRequestHandler<GetEndpointByIdQuery, DataResult<EndpointDto>>
        {
            public async Task<DataResult<EndpointDto>> Handle(GetEndpointByIdQuery request, CancellationToken cancellationToken)
            {
                Endpoint? endpoint = await Repository.GetAsync(predicate: e => e.Id == request.Id, cancellationToken: cancellationToken)
                    ?? throw new NotFoundException(Messages.Endpoint.EndpointNotFound,ExceptionCode.EndpointNotFound);

                EndpointDto endpointDto = Mapper.Map<EndpointDto>(endpoint);

                return new SuccessDataResult<EndpointDto>(endpointDto, Messages.Endpoint.GetEndpointSuccessfully);
            }
        }
    }
}
