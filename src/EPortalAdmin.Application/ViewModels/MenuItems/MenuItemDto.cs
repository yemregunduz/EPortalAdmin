namespace EPortalAdmin.Application.ViewModels.MenuItems
{
    public class MenuItemDto : ViewModelBase
    {
        public string Name { get; set; }
        public string Icon { get; set; }
        public string RouterLink { get; set; }
        public bool IsDropdownMenuItem { get; set; }
        public ICollection<MenuItemDto> SubMenuItems { get; set; }
        public bool HasParent { get; set; }
        public bool HasChildren { get; set; }
    }
}
