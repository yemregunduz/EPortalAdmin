using EPortalAdmin.Application.Features.Endpoints.Rules;
using EPortalAdmin.Application.Features.OperationClaims.Rules;
using EPortalAdmin.Application.Repositories;
using EPortalAdmin.Core.Domain.Entities;
using EPortalAdmin.Core.Domain.Enums;
using EPortalAdmin.Core.Exceptions;
using EPortalAdmin.Domain.Constants;

namespace EPortalAdmin.Application.Features.EndpointOperationClaims.Rules
{
    public class EndpointOperationClaimBusinessRules(IEndpointOperationClaimRepository endpointOperationClaimRepository,
        OperationClaimBusinessRules operationClaimBusinessRules,EndpointBusinessRules endpointBusinessRules)
    {
        public async Task CheckIfOperationClaimExist(int operationClaimId)
        {
            await operationClaimBusinessRules.CheckIfOperationClaimExist(operationClaimId);
        }

        public async Task CheckIfExploreEndpointHasOperationClaim(int endpointId, int operationClaimId)
        {
            EndpointOperationClaim? EndpointOperationClaim =
                await endpointOperationClaimRepository.GetAsync(c => c.EndpointId == endpointId && c.OperationClaimId == operationClaimId, enableTracking: false);

            if (EndpointOperationClaim is not null)
                throw new BusinessException(Messages.EndpointOperationClaim.EndpointOperationClaimAlreadyExist,ExceptionCode.EndpointAlreadyHasOperationClaim);
        }

        public async Task CheckIfExploreEndpointExist(int EndpointId)
        {
            await endpointBusinessRules.CheckIfEndpointExist(EndpointId);
        }

        public async Task ValidateAsync(int endpointId, int operationClaimId)
        {
            await CheckIfExploreEndpointExist(endpointId);
            await CheckIfOperationClaimExist(operationClaimId);
            await CheckIfExploreEndpointHasOperationClaim(endpointId, operationClaimId);
        }
    }
}
