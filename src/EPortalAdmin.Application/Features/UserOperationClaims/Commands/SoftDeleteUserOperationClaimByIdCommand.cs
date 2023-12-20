using EPortalAdmin.Application.ViewModels.UserOperationClaim;
using EPortalAdmin.Application.Wrappers.Results;
using EPortalAdmin.Core.Domain.Entities;
using EPortalAdmin.Core.Domain.Enums;
using EPortalAdmin.Core.Exceptions;
using EPortalAdmin.Domain.Constants;
using MediatR;

namespace EPortalAdmin.Application.Features.UserOperationClaims.Commands
{
    public class SoftDeleteUserOperationClaimByIdCommand : IRequest<DataResult<UserOperationClaimDto>>
    {
        public int Id { get; set; }

        public class SoftDeleteUserOperationClaimByIdCommandHandler : ApplicationFeatureBase<UserOperationClaim>,
            IRequestHandler<SoftDeleteUserOperationClaimByIdCommand, DataResult<UserOperationClaimDto>>
        {
            public async Task<DataResult<UserOperationClaimDto>> Handle(SoftDeleteUserOperationClaimByIdCommand request, CancellationToken cancellationToken)
            {
                UserOperationClaim userOperationClaim =
                    await Repository.GetAsync(m => m.Id == request.Id, cancellationToken: cancellationToken)
                        ?? throw new NotFoundException(Messages.UserOperationClaim.UserOperationClaimNotFound, ExceptionCode.UserOperationClaimNotFound);

                userOperationClaim.MarkAsDelete(CurrentUserId);
                await Repository.SaveChangesAsync(cancellationToken);
                UserOperationClaimDto mappedUserOperationClaim = Mapper.Map<UserOperationClaimDto>(userOperationClaim);

                return new SuccessDataResult<UserOperationClaimDto>(mappedUserOperationClaim, Messages.UserOperationClaim.UserOperationClaimDeleted);
            }
        }
    }
}
