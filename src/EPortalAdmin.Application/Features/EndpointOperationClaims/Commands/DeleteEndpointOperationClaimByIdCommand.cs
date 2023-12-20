using EPortalAdmin.Application.ViewModels.EndpointOperationClaim;
using EPortalAdmin.Application.Wrappers.Results;
using EPortalAdmin.Core.Domain.Entities;
using EPortalAdmin.Domain.Constants;
using MediatR;

namespace EPortalAdmin.Application.Features.EndpointOperationClaims.Commands
{
    public class DeleteEndpointOperationClaimByIdCommand : IRequest<DataResult<EndpointOperationClaimDto>>
    {
        public int Id { get; set; }

        public class DeleteEndpointOperationClaimByIdCommandHandler : ApplicationFeatureBase<EndpointOperationClaim>,
            IRequestHandler<DeleteEndpointOperationClaimByIdCommand, DataResult<EndpointOperationClaimDto>>
        {
            public async Task<DataResult<EndpointOperationClaimDto>> Handle(DeleteEndpointOperationClaimByIdCommand request, CancellationToken cancellationToken)
            {
                EndpointOperationClaim deletedEndpointOperationClaim = await Repository.DeleteByIdAsync(request.Id,cancellationToken);
                EndpointOperationClaimDto endpointOperationClaimDto = Mapper.Map<EndpointOperationClaimDto>(deletedEndpointOperationClaim);

                return new SuccessDataResult<EndpointOperationClaimDto>(endpointOperationClaimDto,
                    Messages.EndpointOperationClaim.EndpointOperationClaimCreated);
            }
        }
    }
}
