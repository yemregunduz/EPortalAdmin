using EPortalAdmin.Application.Features.UserOperationClaims.Rules;
using EPortalAdmin.Application.ViewModels.UserOperationClaim;
using EPortalAdmin.Application.Wrappers.Results;
using EPortalAdmin.Core.Domain.Entities;
using EPortalAdmin.Domain.Constants;
using MediatR;

namespace EPortalAdmin.Application.Features.UserOperationClaims.Commands
{
    public class CreateUserOperationClaimCommand : IRequest<DataResult<UserOperationClaimDto>>
    {
        public int UserId { get; set; }
        public int OperationClaimId { get; set; }

        public class CreateUserOperationClaimCommandHandler(UserOperationClaimBusinessRules userOperationClaimBusinessRules) 
            : ApplicationFeatureBase<UserOperationClaim>, IRequestHandler<CreateUserOperationClaimCommand, DataResult<UserOperationClaimDto>>
        {
            public async Task<DataResult<UserOperationClaimDto>> Handle(CreateUserOperationClaimCommand request, CancellationToken cancellationToken)
            {
                await userOperationClaimBusinessRules.CheckIfUserExist(request.UserId);
                await userOperationClaimBusinessRules.CheckIfOperationClaimExist(request.OperationClaimId);
                await userOperationClaimBusinessRules.CheckIfUserHasOperationClaim(request.UserId, request.OperationClaimId);

                UserOperationClaim userOperationClaim = Mapper.Map<UserOperationClaim>(request);

                UserOperationClaim createdUserOperationClaim = await Repository.AddAsync(userOperationClaim);

                UserOperationClaimDto mappedUserOperationClaim = Mapper.Map<UserOperationClaimDto>(createdUserOperationClaim);

                return new SuccessDataResult<UserOperationClaimDto>(mappedUserOperationClaim, Messages.UserOperationClaim.UserOperationClaimCreated);
            }
        }
    }
}
