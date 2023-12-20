using EPortalAdmin.Application.ViewModels.UserOperationClaim;
using EPortalAdmin.Application.Wrappers.Results;
using EPortalAdmin.Core.Domain.Entities;
using EPortalAdmin.Core.Domain.Enums;
using EPortalAdmin.Core.Exceptions;
using EPortalAdmin.Domain.Constants;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EPortalAdmin.Application.Features.UserOperationClaims.Queries
{
    public class GetUserOperationClaimByIdQuery : IRequest<DataResult<UserOperationClaimDto>>
    {
        public int Id { get; set; }

        public class GetUserOperationClaimByIdQueryHandler : ApplicationFeatureBase<UserOperationClaim>, IRequestHandler<GetUserOperationClaimByIdQuery, DataResult<UserOperationClaimDto>>
        {
            public async Task<DataResult<UserOperationClaimDto>> Handle(GetUserOperationClaimByIdQuery request, CancellationToken cancellationToken)
            {
                UserOperationClaim? userOperationClaim = await Repository.GetAsync(
                    predicate: uoc => uoc.Id == request.Id,
                    include: m => m.Include(uoc => uoc.OperationClaim),
                    cancellationToken: cancellationToken
                    )
                    ?? throw new NotFoundException(Messages.UserOperationClaim.UserOperationClaimNotFound,ExceptionCode.UserOperationClaimNotFound);

                UserOperationClaimDto mappedUserOperationClaim = Mapper.Map<UserOperationClaimDto>(userOperationClaim);
                return new SuccessDataResult<UserOperationClaimDto>(mappedUserOperationClaim, Messages.UserOperationClaim.GetUserOperationClaimSuccessfully);
            }
        }
    }
}
