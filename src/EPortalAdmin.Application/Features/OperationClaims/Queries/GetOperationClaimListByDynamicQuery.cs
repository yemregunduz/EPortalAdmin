using EPortalAdmin.Application.ViewModels.OperationClaim;
using EPortalAdmin.Application.Wrappers.Results;
using EPortalAdmin.Core.Domain.Entities;
using EPortalAdmin.Core.Domain.Models;
using EPortalAdmin.Core.Persistence.Dynamic;
using EPortalAdmin.Core.Persistence.Paging;
using EPortalAdmin.Domain.Constants;
using MediatR;

namespace EPortalAdmin.Application.Features.OperationClaims.Queries
{
    public class GetOperationClaimListByDynamicQuery : IRequest<DataResult<OperationClaimListDto>>
    {
        public PagingRequest PagingRequest { get; set; }
        public Dynamic Dynamic { get; set; }

        public class GetOperationClaimListByDynamicQueryHandler : ApplicationFeatureBase<OperationClaim>, 
            IRequestHandler<GetOperationClaimListByDynamicQuery, DataResult<OperationClaimListDto>>
        {
            public async Task<DataResult<OperationClaimListDto>> Handle(GetOperationClaimListByDynamicQuery request, CancellationToken cancellationToken)
            {
                IPaginate<OperationClaim> operationClaims = await Repository.GetListByDynamicAsync(
                        dynamic: request.Dynamic,
                        index: request.PagingRequest.Page,
                        size: request.PagingRequest.PageSize,
                        cancellationToken: cancellationToken
                        );

                OperationClaimListDto operationClaimList = Mapper.Map<OperationClaimListDto>(operationClaims);

                return new SuccessDataResult<OperationClaimListDto>(operationClaimList, Messages.OperationClaim.OperationClaimsListed);
            }
        }
    }
}
