using EPortalAdmin.Application.ViewModels.OperationClaim;
using EPortalAdmin.Application.Wrappers.Results;
using EPortalAdmin.Core.Domain.Entities;
using EPortalAdmin.Core.Exceptions;
using EPortalAdmin.Domain.Constants;
using MediatR;

namespace EPortalAdmin.Application.Features.OperationClaims.Commands
{
    public class SoftDeleteOperationClaimByNameCommand : IRequest<DataResult<OperationClaimDto>>
    {
        public string Name { get; set; }

        public class SoftDeleteOperationClaimByNameCommandHandler : ApplicationFeatureBase<OperationClaim>,
            IRequestHandler<SoftDeleteOperationClaimByNameCommand, DataResult<OperationClaimDto>>
        {
            public async Task<DataResult<OperationClaimDto>> Handle(SoftDeleteOperationClaimByNameCommand request, CancellationToken cancellationToken)
            {
                OperationClaim operationClaim =
                    await Repository.GetAsync(m => m.Name == request.Name, cancellationToken: cancellationToken)
                        ?? throw new NotFoundException(Messages.OperationClaim.OperationClaimNotFound);

                operationClaim.MarkAsDelete(CurrentUserId);
                await Repository.SaveChangesAsync(cancellationToken);
                OperationClaimDto mappedOperationClaim = Mapper.Map<OperationClaimDto>(operationClaim);

                return new SuccessDataResult<OperationClaimDto>(mappedOperationClaim, Messages.OperationClaim.OperationClaimDeleted);
            }
        }
    }
}
