using EPortalAdmin.Application.ViewModels.OperationClaim;
using EPortalAdmin.Application.Wrappers.Results;
using EPortalAdmin.Core.Domain.Entities;
using EPortalAdmin.Domain.Constants;
using MediatR;

namespace EPortalAdmin.Application.Features.OperationClaims.Commands
{
    public class DeleteOperationClaimByIdCommand : IRequest<DataResult<OperationClaimDto>>
    {
        public int Id { get; set; }



        public class DeleteOperationClaimByIdCommandHandler : ApplicationFeatureBase<OperationClaim>, IRequestHandler<DeleteOperationClaimByIdCommand, DataResult<OperationClaimDto>>
        {
            public async Task<DataResult<OperationClaimDto>> Handle(DeleteOperationClaimByIdCommand request, CancellationToken cancellationToken)
            {
                OperationClaim? deletedOperationClaim = await Repository.DeleteByIdAsync(request.Id);
                OperationClaimDto mappedOperationClaim = Mapper.Map<OperationClaimDto>(deletedOperationClaim);

                return new SuccessDataResult<OperationClaimDto>(mappedOperationClaim, Messages.OperationClaim.OperationClaimDeleted);
            }
        }
    }
}
