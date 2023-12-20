using EPortalAdmin.Application.Repositories;
using EPortalAdmin.Core.Domain.Entities;
using EPortalAdmin.Core.Domain.Enums;
using EPortalAdmin.Core.Exceptions;
using EPortalAdmin.Domain.Constants;

namespace EPortalAdmin.Application.Features.OperationClaims.Rules
{
    public class OperationClaimBusinessRules(IOperationClaimRepository operationClaimRepository)
    {
        public async Task OperationClaimNameCanNotBeDuplicated(string name)
        {
            OperationClaim? operationClaim = await operationClaimRepository.GetAsync(o => o.Name == name);

            if (operationClaim != null)
                throw new BusinessException(Messages.OperationClaim.OperationClaimAlreadyExist,ExceptionCode.OperationClaimAlreadyExist);
        }

        public async Task CheckIfOperationClaimExist(int operationClaimId)
        {
            OperationClaim? operationClaim = await operationClaimRepository.GetAsync(
                o => o.Id == operationClaimId, enableTracking: false)
                ?? throw new NotFoundException(Messages.OperationClaim.OperationClaimNotFound);
        }
    }
}
