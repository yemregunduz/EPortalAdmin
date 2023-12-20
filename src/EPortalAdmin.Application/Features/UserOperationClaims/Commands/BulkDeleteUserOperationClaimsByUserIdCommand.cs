using EPortalAdmin.Application.Wrappers.Results;
using EPortalAdmin.Core.Domain.Entities;
using EPortalAdmin.Core.Domain.Enums;
using EPortalAdmin.Core.Exceptions;
using EPortalAdmin.Domain.Constants;
using MediatR;

namespace EPortalAdmin.Application.Features.UserOperationClaims.Commands
{
    public class BulkDeleteUserOperationClaimsByUserIdCommand : IRequest<Result>
    {
        public int UserId { get; set; }
        public class BulkDeleteUserOperationClaimsByUserIdCommandHandler : ApplicationFeatureBase<UserOperationClaim>, IRequestHandler<BulkDeleteUserOperationClaimsByUserIdCommand, Result>
        {
            public async Task<Result> Handle(BulkDeleteUserOperationClaimsByUserIdCommand request, CancellationToken cancellationToken)
            {
                IList<UserOperationClaim> userOperationClaims = await Repository.GetAllAsync(
                    predicate: uoc => uoc.UserId == request.UserId,
                    cancellationToken: cancellationToken);

                if (userOperationClaims is null || userOperationClaims.Count == 0)
                    throw new NotFoundException(Messages.UserOperationClaim.UserOperationClaimNotFound, ExceptionCode.UserOperationClaimNotFound);

                var result = await Repository.DeleteRangeAsync(userOperationClaims);

                return result ? new SuccessResult(Messages.UserOperationClaim.AllOperationClaimsDeletedOnUser) : new ErrorResult();
            }
        }
    }
}
