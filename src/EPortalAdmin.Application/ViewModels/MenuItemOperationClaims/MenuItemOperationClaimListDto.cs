using EPortalAdmin.Core.Persistence.Paging;

namespace EPortalAdmin.Application.ViewModels.MenuItemOperationClaims
{
    public class MenuItemOperationClaimListDto : BasePageableModel
    {
        public IList<MenuItemOperationClaimDto> Items { get; set; }
    }
}
