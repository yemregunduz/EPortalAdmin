using EPortalAdmin.Application.Features.OperationClaims.Rules;
using EPortalAdmin.Application.ViewModels.OperationClaim;
using EPortalAdmin.Application.Wrappers.Results;
using EPortalAdmin.Core.Domain.Entities;
using EPortalAdmin.Domain.Constants;
using MediatR;

namespace EPortalAdmin.Application.Features.OperationClaims.Commands
{
    public class CreateOperationClaimCommand : IRequest<DataResult<OperationClaimDto>>
    {
        public string Name { get; set; }


        public class CreateOperationClaimCommandHandler : ApplicationFeatureBase<OperationClaim>, IRequestHandler<CreateOperationClaimCommand, DataResult<OperationClaimDto>>
        {
            private readonly OperationClaimBusinessRules _operationClaimRules;

            public CreateOperationClaimCommandHandler(OperationClaimBusinessRules operationClaimRules)
            {
                _operationClaimRules = operationClaimRules;
            }

            public async Task<DataResult<OperationClaimDto>> Handle(CreateOperationClaimCommand request, CancellationToken cancellationToken)
            {
                await _operationClaimRules.OperationClaimNameCanNotBeDuplicated(request.Name);

                OperationClaim operationClaim = Mapper.Map<OperationClaim>(request);
                OperationClaim createdOperationClaim = await Repository.AddAsync(operationClaim);
                OperationClaimDto mappedOperationClaim = Mapper.Map<OperationClaimDto>(createdOperationClaim);

                return new SuccessDataResult<OperationClaimDto>(mappedOperationClaim, Messages.OperationClaim.OperationClaimCreated);
            }
        }
    }
}
