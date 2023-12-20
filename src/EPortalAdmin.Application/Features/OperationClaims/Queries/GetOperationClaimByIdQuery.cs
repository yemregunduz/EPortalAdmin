using EPortalAdmin.Application.ViewModels.OperationClaim;
using EPortalAdmin.Application.Wrappers.Results;
using EPortalAdmin.Core.Domain.Entities;
using EPortalAdmin.Core.Domain.Enums;
using EPortalAdmin.Core.Exceptions;
using EPortalAdmin.Domain.Constants;
using MediatR;

namespace EPortalAdmin.Application.Features.OperationClaims.Queries
{
    public class GetOperationClaimByIdQuery : IRequest<DataResult<OperationClaimDto>>
    {
        public int Id { get; set; }

        public class GetOperationClaimByIdQueryHandler : ApplicationFeatureBase<OperationClaim>, IRequestHandler<GetOperationClaimByIdQuery, DataResult<OperationClaimDto>>
        {
            public async Task<DataResult<OperationClaimDto>> Handle(GetOperationClaimByIdQuery request, CancellationToken cancellationToken)
            {
                OperationClaim? operationClaim = await Repository.GetAsync(o => o.Id == request.Id, cancellationToken:cancellationToken)
                    ?? throw new NotFoundException(Messages.OperationClaim.OperationClaimNotFound, ExceptionCode.OperationClaimNotFound);

                OperationClaimDto mappedOperationClaim = Mapper.Map<OperationClaimDto>(operationClaim);

                return new SuccessDataResult<OperationClaimDto>(mappedOperationClaim, Messages.OperationClaim.OperationClaimGetSuccessfully);

            }
        }
    }
}
