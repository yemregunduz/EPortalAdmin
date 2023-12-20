using EPortalAdmin.Application.Wrappers.Results;
using EPortalAdmin.Core.Domain.Entities;
using EPortalAdmin.Core.Domain.Enums;
using EPortalAdmin.Core.Exceptions;
using EPortalAdmin.Domain.Constants;
using MediatR;

namespace EPortalAdmin.Application.Features.UserOperationClaims.Commands
{
    public class SoftBulkDeleteUserOperationClaimsByOperationClaimId : IRequest<Result>
    {
        public int OperationClaimId { get; set; }
        public class SoftBulkDeleteUserOperationClaimsByOperationClaimIdHandler :ApplicationFeatureBase<UserOperationClaim>, 
            IRequestHandler<SoftBulkDeleteUserOperationClaimsByOperationClaimId, Result>
        {
            public async Task<Result> Handle(SoftBulkDeleteUserOperationClaimsByOperationClaimId request, CancellationToken cancellationToken)
            {
                IList<UserOperationClaim> userOperationClaims = await Repository.GetAllAsync(
                    predicate: uoc => uoc.OperationClaimId == request.OperationClaimId, cancellationToken: cancellationToken);

                if (userOperationClaims is null || userOperationClaims.Count == 0)
                    throw new NotFoundException(Messages.UserOperationClaim.UserOperationClaimNotFound,ExceptionCode.UserOperationClaimNotFound);

                foreach (UserOperationClaim userOperationClaim in userOperationClaims)
                {
                    userOperationClaim.MarkAsDelete(CurrentUserId);
                }

                await Repository.SaveChangesAsync(cancellationToken);

                return new SuccessResult(Messages.UserOperationClaim.OperationClaimDeletedAllUser);
            }
        }
    }
}
