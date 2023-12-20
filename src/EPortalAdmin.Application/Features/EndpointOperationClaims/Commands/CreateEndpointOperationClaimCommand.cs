using EPortalAdmin.Application.Features.EndpointOperationClaims.Rules;
using EPortalAdmin.Application.ViewModels.EndpointOperationClaim;
using EPortalAdmin.Application.Wrappers.Results;
using EPortalAdmin.Core.Domain.Entities;
using EPortalAdmin.Domain.Constants;
using MediatR;

namespace EPortalAdmin.Application.Features.EndpointOperationClaims.Commands
{
    public class CreateEndpointOperationClaimCommand : IRequest<DataResult<EndpointOperationClaimDto>>
    {
        public int EndpointId { get; set; }
        public int OperationClaimId { get; set; }


        public class CreateEndpointOperationClaimCommandHandler(EndpointOperationClaimBusinessRules endpointOperationClaimBusinessRules) 
            : ApplicationFeatureBase<EndpointOperationClaim>,IRequestHandler<CreateEndpointOperationClaimCommand, DataResult<EndpointOperationClaimDto>>
        {
            public async Task<DataResult<EndpointOperationClaimDto>> Handle(CreateEndpointOperationClaimCommand request, CancellationToken cancellationToken)
            {
                await endpointOperationClaimBusinessRules.ValidateAsync(request.EndpointId, request.OperationClaimId);

                EndpointOperationClaim endpointOperationClaim = Mapper.Map<EndpointOperationClaim>(request);
                EndpointOperationClaim createdEndpointOperationClaim = await Repository.AddAsync(endpointOperationClaim);
                EndpointOperationClaimDto mappedEndpointOperationClaim = Mapper.Map<EndpointOperationClaimDto>(createdEndpointOperationClaim);

                return new SuccessDataResult<EndpointOperationClaimDto>(mappedEndpointOperationClaim, Messages.EndpointOperationClaim.EndpointOperationClaimCreated);
            }
        }
    }
}
