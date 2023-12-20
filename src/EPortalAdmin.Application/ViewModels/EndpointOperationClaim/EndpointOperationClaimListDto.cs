using EPortalAdmin.Core.Persistence.Paging;

namespace EPortalAdmin.Application.ViewModels.EndpointOperationClaim
{
    public class EndpointOperationClaimListDto : BasePageableModel
    {
        public IList<EndpointOperationClaimDto> Items { get; set; }
    }
}
