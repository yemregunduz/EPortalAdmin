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

        public class CreateUserOperationClaimCommandHandler : ApplicationFeatureBase<UserOperationClaim>, IRequestHandler<CreateUserOperationClaimCommand, DataResult<UserOperationClaimDto>>
        {
            private readonly UserOperationClaimBusinessRules _userOperationClaimBusinessRules;

            public CreateUserOperationClaimCommandHandler(UserOperationClaimBusinessRules userOperationClaimBusinessRules)
            {
                _userOperationClaimBusinessRules = userOperationClaimBusinessRules;
            }

            public async Task<DataResult<UserOperationClaimDto>> Handle(CreateUserOperationClaimCommand request, CancellationToken cancellationToken)
            {
                await _userOperationClaimBusinessRules.CheckIfUserExist(request.UserId);
                await _userOperationClaimBusinessRules.CheckIfOperationClaimExist(request.OperationClaimId);
                await _userOperationClaimBusinessRules.CheckIfUserHasOperationClaim(request.UserId, request.OperationClaimId);

                UserOperationClaim userOperationClaim = Mapper.Map<UserOperationClaim>(request);

                UserOperationClaim createdUserOperationClaim = await Repository.AddAsync(userOperationClaim);

                UserOperationClaimDto mappedUserOperationClaim = Mapper.Map<UserOperationClaimDto>(createdUserOperationClaim);

                return new SuccessDataResult<UserOperationClaimDto>(mappedUserOperationClaim, Messages.UserOperationClaim.UserOperationClaimCreated);
            }
        }
    }
}
