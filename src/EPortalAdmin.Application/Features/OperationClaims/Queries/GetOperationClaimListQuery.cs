using EPortalAdmin.Application.ViewModels.OperationClaim;
using EPortalAdmin.Application.Wrappers.Results;
using EPortalAdmin.Core.Domain.Entities;
using EPortalAdmin.Core.Domain.Models;
using EPortalAdmin.Core.Persistence.Paging;
using EPortalAdmin.Domain.Constants;
using MediatR;

namespace EPortalAdmin.Application.Features.OperationClaims.Queries
{
    public class GetOperationClaimListQuery : IRequest<DataResult<OperationClaimListDto>>
    {
        public PagingRequest PagingRequest { get; set; }

        public class GetOperationClaimListQueryHandler : ApplicationFeatureBase<OperationClaim>, IRequestHandler<GetOperationClaimListQuery, DataResult<OperationClaimListDto>>
        {
            public async Task<DataResult<OperationClaimListDto>> Handle(GetOperationClaimListQuery request, CancellationToken cancellationToken)
            {
                IPaginate<OperationClaim> operationClaims = await Repository.GetListAsync(
                    index: request.PagingRequest.Page,
                    size: request.PagingRequest.PageSize,
                    cancellationToken: cancellationToken);

                OperationClaimListDto operationClaimList = Mapper.Map<OperationClaimListDto>(operationClaims);

                return new SuccessDataResult<OperationClaimListDto>(operationClaimList, Messages.OperationClaim.OperationClaimsListed);
            }
        }
    }
}
