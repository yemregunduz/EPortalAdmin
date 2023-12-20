using EPortalAdmin.Core.Persistence.Paging;

namespace EPortalAdmin.Application.ViewModels.UserOperationClaim
{
    public class UserOperationClaimListDto : BasePageableModel
    {
        public IList<UserOperationClaimDto> Items { get; set; }
    }
}
