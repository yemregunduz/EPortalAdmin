using EPortalAdmin.Application.Features.OperationClaims.Rules;
using EPortalAdmin.Application.Features.Users.Rules;
using EPortalAdmin.Application.Repositories;
using EPortalAdmin.Core.Domain.Entities;
using EPortalAdmin.Core.Exceptions;
using EPortalAdmin.Domain.Constants;

namespace EPortalAdmin.Application.Features.UserOperationClaims.Rules
{
    public class UserOperationClaimBusinessRules
    {
        private readonly IUserOperationClaimRepository _userOperationClaimRepository;
        private readonly OperationClaimBusinessRules _operationClaimBusinessRules;
        private readonly UserBusinessRules _userBusinessRules;
        public UserOperationClaimBusinessRules(IUserOperationClaimRepository userOperationClaimRepository, IOperationClaimRepository operationClaimRepository, OperationClaimBusinessRules operationClaimBusinessRules, UserBusinessRules userBusinessRules)
        {
            _userOperationClaimRepository = userOperationClaimRepository;
            _operationClaimBusinessRules = operationClaimBusinessRules;
            _userBusinessRules = userBusinessRules;
        }

        public async Task CheckIfUserHasOperationClaim(int userId, int operationClaimId)
        {
            UserOperationClaim? userOperationClaim = await _userOperationClaimRepository.GetAsync(
                uoc => uoc.OperationClaimId == operationClaimId && uoc.UserId == userId);
            if (userOperationClaim is not null)
                throw new BusinessException(Messages.UserOperationClaim.UserAlreadyHasOperationClaim);
        }

        public async Task CheckIfUserExist(int userId)
        {
            await _userBusinessRules.CheckIfUserExist(userId);
        }

        public async Task CheckIfOperationClaimExist(int operationClaimId)
        {
            await _operationClaimBusinessRules.CheckIfOperationClaimExist(operationClaimId);
        }
    }
}
