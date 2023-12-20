using EPortalAdmin.Application.Repositories;
using EPortalAdmin.Core.Domain.Entities;
using EPortalAdmin.Core.Domain.Enums;
using EPortalAdmin.Core.Exceptions;
using EPortalAdmin.Domain.Constants;

namespace EPortalAdmin.Application.Features.Endpoints.Rules
{
    public class EndpointBusinessRules(IEndpointRepository endpointRepository)
    {
        public async Task CheckIfEndpointExist(int endpointId)
        {
            Endpoint? endpoint = await endpointRepository.GetAsync(e => e.Id == endpointId)
                ?? throw new BusinessException(Messages.EndpointOperationClaim.EndpointDoesNotExist, ExceptionCode.EndpointNotFound);
        }
    }
}
