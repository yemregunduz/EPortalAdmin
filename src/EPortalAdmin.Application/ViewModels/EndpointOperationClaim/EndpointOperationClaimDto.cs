using EPortalAdmin.Application.ViewModels.OperationClaim;

namespace EPortalAdmin.Application.ViewModels.EndpointOperationClaim
{
    public class EndpointOperationClaimDto : ViewModelBase
    {
        public int EndpointId { get; set; }
        public OperationClaimDto OperationClaimDto { get; set; }
        public int OperationClaimId { get; set; }
    }
}
