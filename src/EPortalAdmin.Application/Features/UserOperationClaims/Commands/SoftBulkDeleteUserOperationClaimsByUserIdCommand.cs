using EPortalAdmin.Application.Wrappers.Results;
using EPortalAdmin.Core.Domain.Entities;
using EPortalAdmin.Core.Domain.Enums;
using EPortalAdmin.Core.Exceptions;
using EPortalAdmin.Domain.Constants;
using MediatR;

namespace EPortalAdmin.Application.Features.UserOperationClaims.Commands
{
    public class SoftBulkDeleteUserOperationClaimsByUserId : IRequest<Result>
    {
        public int UserId { get; set; }
        public class SoftBulkDeleteUserOperationClaimsByUserIdHandler : ApplicationFeatureBase<UserOperationClaim>, IRequestHandler<SoftBulkDeleteUserOperationClaimsByUserId, Result>
        {
            public async Task<Result> Handle(SoftBulkDeleteUserOperationClaimsByUserId request, CancellationToken cancellationToken)
            {
                IList<UserOperationClaim> userOperationClaims = await Repository.GetAllAsync(
                    predicate: uoc => uoc.UserId == request.UserId, cancellationToken: cancellationToken);

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
