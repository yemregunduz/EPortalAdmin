using EPortalAdmin.Application.Wrappers.Results;
using EPortalAdmin.Core.Domain.Entities;
using EPortalAdmin.Core.Domain.Enums;
using EPortalAdmin.Core.Exceptions;
using EPortalAdmin.Domain.Constants;
using MediatR;

namespace EPortalAdmin.Application.Features.UserOperationClaims.Commands
{
    public class BulkDeleteUserOperationClaimsByOperationClaimIdCommand : IRequest<Result>
    {
        public int OperationClaimId { get; set; }
        public class BulkDeleteUserOperationClaimsByOperationClaimIdCommandHandler : ApplicationFeatureBase<UserOperationClaim>, 
            IRequestHandler<BulkDeleteUserOperationClaimsByOperationClaimIdCommand, Result>
        {
            public async Task<Result> Handle(BulkDeleteUserOperationClaimsByOperationClaimIdCommand request, CancellationToken cancellationToken)
            {
                IList<UserOperationClaim> userOperationClaims = await Repository.GetAllAsync(
                    predicate: uoc => uoc.OperationClaimId == request.OperationClaimId, cancellationToken: cancellationToken);

                if (userOperationClaims is null || userOperationClaims.Count == 0)
                    throw new NotFoundException(Messages.UserOperationClaim.UserOperationClaimNotFound, ExceptionCode.UserOperationClaimNotFound);

                var result = await Repository.DeleteRangeAsync(userOperationClaims);

                return result ? new SuccessResult(Messages.UserOperationClaim.OperationClaimDeletedAllUser) : new ErrorResult();
            }
        }
    }
}
