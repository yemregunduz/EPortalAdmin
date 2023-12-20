using EPortalAdmin.Application.ViewModels.OperationClaim;
using EPortalAdmin.Application.Wrappers.Results;
using EPortalAdmin.Core.Domain.Entities;
using EPortalAdmin.Domain.Constants;
using MediatR;


namespace EPortalAdmin.Application.Features.OperationClaims.Commands
{
    public class DeleteOperationClaimByNameCommand : IRequest<DataResult<OperationClaimDto>>
    {
        public string Name { get; set; }
        public class DeleteOperationClaimByNameCommandHandler : ApplicationFeatureBase<OperationClaim>, IRequestHandler<DeleteOperationClaimByNameCommand, DataResult<OperationClaimDto>>
        {
            public async Task<DataResult<OperationClaimDto>> Handle(DeleteOperationClaimByNameCommand request, CancellationToken cancellationToken)
            {
                OperationClaim? operationClaim = await Repository.DeleteByPredicateAsync(o => o.Name == request.Name, cancellationToken);
                OperationClaimDto mappedOperationClaim = Mapper.Map<OperationClaimDto>(operationClaim);

                return new SuccessDataResult<OperationClaimDto>(mappedOperationClaim, Messages.OperationClaim.OperationClaimDeleted);
            }
        }
    }
}
