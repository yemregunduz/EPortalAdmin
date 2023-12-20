using EPortalAdmin.Application.Features.EndpointOperationClaims.Rules;
using EPortalAdmin.Application.ViewModels.EndpointOperationClaim;
using EPortalAdmin.Application.Wrappers.Results;
using EPortalAdmin.Core.Domain.Entities;
using EPortalAdmin.Core.Domain.Enums;
using EPortalAdmin.Core.Exceptions;
using EPortalAdmin.Domain.Constants;
using MediatR;

namespace EPortalAdmin.Application.Features.EndpointOperationClaims.Commands
{
    public class UpdateEndpointOperationClaimCommand : IRequest<DataResult<EndpointOperationClaimDto>>
    {
        public int Id { get; set; }
        public int EndpointId { get; set; }
        public int OperationClaimId { get; set; }

        public class UpdateEndpointOperationClaimCommandHandler(EndpointOperationClaimBusinessRules endpointOperationClaimBusinessRules) 
            : ApplicationFeatureBase<EndpointOperationClaim>, IRequestHandler<UpdateEndpointOperationClaimCommand, DataResult<EndpointOperationClaimDto>>
        {
            public async Task<DataResult<EndpointOperationClaimDto>> Handle(UpdateEndpointOperationClaimCommand request, CancellationToken cancellationToken)
            {
                await endpointOperationClaimBusinessRules.ValidateAsync(request.EndpointId, request.OperationClaimId);

                EndpointOperationClaim endpointOperationClaim = await Repository.GetAsync(e=>e.Id == request.Id,cancellationToken:cancellationToken)
                    ?? throw new NotFoundException(Messages.EndpointOperationClaim.EndpointOperationClaimNotFound, ExceptionCode.EndpointOperationClaimNotFound);

                Mapper.Map(request, endpointOperationClaim);
                EndpointOperationClaim updatedEndpointOperationClaim = await Repository.UpdateAsync(endpointOperationClaim, cancellationToken);
                EndpointOperationClaimDto endpointOperationClaimDto = Mapper.Map<EndpointOperationClaimDto>(updatedEndpointOperationClaim);

                return new SuccessDataResult<EndpointOperationClaimDto>(endpointOperationClaimDto, Messages.EndpointOperationClaim.EndpointUpdated);
            }
        }
    }
}
