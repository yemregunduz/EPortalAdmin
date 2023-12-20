using EPortalAdmin.Core.Persistence.Paging;

namespace EPortalAdmin.Application.ViewModels.OperationClaim
{
    public class OperationClaimListDto : BasePageableModel
    {
        public IList<OperationClaimDto> Items { get; set; }
    }
}
