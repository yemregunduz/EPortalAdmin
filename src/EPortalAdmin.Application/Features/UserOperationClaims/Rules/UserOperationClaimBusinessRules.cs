using EPortalAdmin.Application.Features.OperationClaims.Rules;
using EPortalAdmin.Application.Features.Users.Rules;
using EPortalAdmin.Application.Repositories;
using EPortalAdmin.Core.Domain.Entities;
using EPortalAdmin.Core.Domain.Enums;
using EPortalAdmin.Core.Exceptions;
using EPortalAdmin.Domain.Constants;

namespace EPortalAdmin.Application.Features.UserOperationClaims.Rules
{
    public class UserOperationClaimBusinessRules(IUserOperationClaimRepository userOperationClaimRepository, 
        OperationClaimBusinessRules operationClaimBusinessRules, UserBusinessRules userBusinessRules)
    {

        public async Task CheckIfUserHasOperationClaim(int userId, int operationClaimId)
        {
            UserOperationClaim? userOperationClaim = await userOperationClaimRepository.GetAsync(
                uoc => uoc.OperationClaimId == operationClaimId && uoc.UserId == userId);
            if (userOperationClaim is not null)
                throw new BusinessException(Messages.UserOperationClaim.UserAlreadyHasOperationClaim,ExceptionCode.UserAlreadyHasOperationClaim);
        }

        public async Task CheckIfUserExist(int userId)
        {
            await userBusinessRules.CheckIfUserExist(userId);
        }

        public async Task CheckIfOperationClaimExist(int operationClaimId)
        {
            await operationClaimBusinessRules.CheckIfOperationClaimExist(operationClaimId);
        }
    }
}
