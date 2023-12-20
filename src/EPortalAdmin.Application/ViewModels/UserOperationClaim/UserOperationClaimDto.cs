using EPortalAdmin.Application.ViewModels.OperationClaim;

namespace EPortalAdmin.Application.ViewModels.UserOperationClaim
{
    public class UserOperationClaimDto : ViewModelBase
    {
        public int UserId { get; set; }
        public OperationClaimDto OperationClaimDto { get; set; }
        public int OperationClaimId { get; set; }
    }
}
