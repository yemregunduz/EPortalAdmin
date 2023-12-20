using EPortalAdmin.Application.Features.UserOperationClaims.Rules;
using EPortalAdmin.Application.ViewModels.UserOperationClaim;
using EPortalAdmin.Application.Wrappers.Results;
using EPortalAdmin.Core.Domain.Entities;
using EPortalAdmin.Core.Exceptions;
using EPortalAdmin.Domain.Constants;
using MediatR;

namespace EPortalAdmin.Application.Features.UserOperationClaims.Commands
{
    public class UpdateUserOperationClaimCommand : IRequest<DataResult<UserOperationClaimDto>>
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int OperationClaimId { get; set; }

        public class UpdateUserOperationClaimCommandHandler(UserOperationClaimBusinessRules userOperationClaimBusinessRules)
            : ApplicationFeatureBase<UserOperationClaim>, IRequestHandler<UpdateUserOperationClaimCommand, DataResult<UserOperationClaimDto>>
        {
            public async Task<DataResult<UserOperationClaimDto>> Handle(UpdateUserOperationClaimCommand request, CancellationToken cancellationToken)
            {
                await userOperationClaimBusinessRules.CheckIfUserExist(request.UserId);
                await userOperationClaimBusinessRules.CheckIfOperationClaimExist(request.OperationClaimId);
                await userOperationClaimBusinessRules.CheckIfUserHasOperationClaim(request.UserId, request.OperationClaimId);

                UserOperationClaim? userOperationClaim = await Repository.GetAsync(uoc => uoc.Id == request.Id)
                ?? throw new NotFoundException(Messages.UserOperationClaim.UserOperationClaimNotFound);

                Mapper.Map(request, userOperationClaim);

                UserOperationClaim updatedUserOperationClaim = await Repository.UpdateAsync(userOperationClaim);

                UserOperationClaimDto mappedOperationClaim = Mapper.Map<UserOperationClaimDto>(updatedUserOperationClaim);

                return new SuccessDataResult<UserOperationClaimDto>(mappedOperationClaim, Messages.UserOperationClaim.UserOperationClaimUpdated);
            }
        }
    }
}
