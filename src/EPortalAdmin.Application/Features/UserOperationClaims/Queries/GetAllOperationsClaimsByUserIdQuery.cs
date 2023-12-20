using EPortalAdmin.Application.Features.UserOperationClaims.Rules;
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
    public class GetAllOperationsClaimsByUserIdQuery : IRequest<DataResult<IList<UserOperationClaimDto>>>
    {
        public int UserId { get; set; }

        public class GetAllOperationsClaimsByUserIdQueryHandler : ApplicationFeatureBase<UserOperationClaim>, 
            IRequestHandler<GetAllOperationsClaimsByUserIdQuery, DataResult<IList<UserOperationClaimDto>>>
        {
            private readonly UserOperationClaimBusinessRules _businessRules;

            public GetAllOperationsClaimsByUserIdQueryHandler(UserOperationClaimBusinessRules businessRules)
            {
                _businessRules = businessRules;
            }

            public async Task<DataResult<IList<UserOperationClaimDto>>> Handle(GetAllOperationsClaimsByUserIdQuery request, CancellationToken cancellationToken)
            {
                await _businessRules.CheckIfUserExist(request.UserId);

                IList<UserOperationClaim>? userOperationClaims = await Repository.GetAllAsync(
                    predicate: uoc => uoc.UserId == request.UserId,
                    include: m => m.Include(uoc => uoc.OperationClaim),
                    cancellationToken: cancellationToken)
                    ?? throw new NotFoundException(Messages.UserOperationClaim.UserOperationClaimNotFound,ExceptionCode.UserOperationClaimNotFound);

                IList<UserOperationClaimDto> mappedUserOperationClaims = Mapper.Map<IList<UserOperationClaimDto>>(userOperationClaims);
                return new SuccessDataResult<IList<UserOperationClaimDto>>(mappedUserOperationClaims, Messages.UserOperationClaim.GetUserOperationClaimSuccessfully);
            }
        }
    }
}
