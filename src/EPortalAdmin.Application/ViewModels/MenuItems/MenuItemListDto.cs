using EPortalAdmin.Core.Persistence.Paging;

namespace EPortalAdmin.Application.ViewModels.MenuItems
{
    public class MenuItemListDto : BasePageableModel
    {
        public IList<MenuItemDto> Items { get; set; }
    }
}
