using EPortalAdmin.Application.ViewModels.UserOperationClaim;
using EPortalAdmin.Application.Wrappers.Results;
using EPortalAdmin.Core.Domain.Entities;
using EPortalAdmin.Domain.Constants;
using MediatR;

namespace EPortalAdmin.Application.Features.UserOperationClaims.Commands
{
    public class DeleteUserOperationClaimByIdCommand : IRequest<DataResult<UserOperationClaimDto>>
    {
        public int Id { get; set; }

        public class DeleteUserOperationClaimByIdCommandHandler : ApplicationFeatureBase<UserOperationClaim>, 
            IRequestHandler<DeleteUserOperationClaimByIdCommand, DataResult<UserOperationClaimDto>>
        {
            public async Task<DataResult<UserOperationClaimDto>> Handle(DeleteUserOperationClaimByIdCommand request, CancellationToken cancellationToken)
            {
                UserOperationClaim? deletedUserOperationClaim = await Repository.DeleteByIdAsync(request.Id);
                UserOperationClaimDto userOperationClaimDto = Mapper.Map<UserOperationClaimDto>(deletedUserOperationClaim);
                return new SuccessDataResult<UserOperationClaimDto>(userOperationClaimDto, Messages.UserOperationClaim.UserOperationClaimDeleted);
            }
        }
    }
}
