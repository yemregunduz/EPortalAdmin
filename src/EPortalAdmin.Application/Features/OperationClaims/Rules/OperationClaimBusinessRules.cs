using EPortalAdmin.Application.Repositories;
using EPortalAdmin.Core.Domain.Entities;
using EPortalAdmin.Core.Exceptions;
using EPortalAdmin.Domain.Constants;

namespace EPortalAdmin.Application.Features.OperationClaims.Rules
{
    public class OperationClaimBusinessRules
    {
        private readonly IOperationClaimRepository _operationClaimRepository;

        public OperationClaimBusinessRules(IOperationClaimRepository operationClaimRepository)
        {
            _operationClaimRepository = operationClaimRepository;
        }

        public async Task OperationClaimNameCanNotBeDuplicated(string name)
        {
            OperationClaim? operationClaim = await _operationClaimRepository.GetAsync(o => o.Name == name);

            if (operationClaim != null)
                throw new BusinessException(Messages.OperationClaim.OperationClaimAlreadyExist);
        }

        public async Task CheckIfOperationClaimExist(int operationClaimId)
        {
            OperationClaim? operationClaim = await _operationClaimRepository.GetAsync(
                o => o.Id == operationClaimId, enableTracking: false)
                ?? throw new NotFoundException(Messages.OperationClaim.OperationClaimNotFound);
        }
    }
}
