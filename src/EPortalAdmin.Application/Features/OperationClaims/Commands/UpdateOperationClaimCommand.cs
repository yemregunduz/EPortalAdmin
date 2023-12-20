using EPortalAdmin.Application.Features.OperationClaims.Rules;
using EPortalAdmin.Application.ViewModels.OperationClaim;
using EPortalAdmin.Application.Wrappers.Results;
using EPortalAdmin.Core.Domain.Entities;
using EPortalAdmin.Core.Exceptions;
using EPortalAdmin.Domain.Constants;
using MediatR;

namespace EPortalAdmin.Application.Features.OperationClaims.Commands
{
    public class UpdateOperationClaimCommand : IRequest<DataResult<OperationClaimDto>>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public class UpdateOperationClaimCommandHandler : ApplicationFeatureBase<OperationClaim>, IRequestHandler<UpdateOperationClaimCommand, DataResult<OperationClaimDto>>
        {
            private readonly OperationClaimBusinessRules _businessRules;
            public UpdateOperationClaimCommandHandler(OperationClaimBusinessRules businessRules)
            {
                _businessRules = businessRules;
            }

            public async Task<DataResult<OperationClaimDto>> Handle(UpdateOperationClaimCommand request, CancellationToken cancellationToken)
            {
                await _businessRules.OperationClaimNameCanNotBeDuplicated(request.Name);

                OperationClaim? operationClaim = await Repository.GetAsync(o => o.Id == request.Id, cancellationToken: cancellationToken)
                    ?? throw new NotFoundException(Messages.OperationClaim.OperationClaimNotFound);

                Mapper.Map(request, operationClaim);

                OperationClaim updatedOperationClaim = await Repository.UpdateAsync(operationClaim);

                OperationClaimDto mappedOperationClaim = Mapper.Map<OperationClaimDto>(updatedOperationClaim);

                return new SuccessDataResult<OperationClaimDto>(mappedOperationClaim, Messages.OperationClaim.OperationClaimUpdated);
            }
        }
    }
}
