using EPortalAdmin.Application.ViewModels.OperationClaim;
using EPortalAdmin.Application.Wrappers.Results;
using EPortalAdmin.Core.Domain.Entities;
using EPortalAdmin.Core.Domain.Enums;
using EPortalAdmin.Core.Exceptions;
using EPortalAdmin.Domain.Constants;
using MediatR;

namespace EPortalAdmin.Application.Features.OperationClaims.Commands
{
    public class SoftDeleteOperationClaimByIdCommand : IRequest<DataResult<OperationClaimDto>>
    {
        public int Id { get; set; }

        public class SoftDeleteOperationClaimByIdCommandHandler : ApplicationFeatureBase<OperationClaim>, IRequestHandler<SoftDeleteOperationClaimByIdCommand, DataResult<OperationClaimDto>>
        {
            public async Task<DataResult<OperationClaimDto>> Handle(SoftDeleteOperationClaimByIdCommand request, CancellationToken cancellationToken)
            {
                OperationClaim operationClaim = await Repository.GetAsync(m => m.Id == request.Id, cancellationToken: cancellationToken)
                        ?? throw new NotFoundException(Messages.OperationClaim.OperationClaimNotFound, ExceptionCode.OperationClaimNotFound);

                operationClaim.MarkAsDelete(CurrentUserId);
                await Repository.SaveChangesAsync(cancellationToken);
                OperationClaimDto mappedOperationClaim = Mapper.Map<OperationClaimDto>(operationClaim);

                return new SuccessDataResult<OperationClaimDto>(mappedOperationClaim, Messages.OperationClaim.OperationClaimDeleted);
            }
        }
    }
}
