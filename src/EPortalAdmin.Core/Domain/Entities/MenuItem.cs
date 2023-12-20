namespace EPortalAdmin.Core.Domain.Entities
{
    public class MenuItem : BaseEntity
    {
        public string Name { get; set; }
        public string? Icon { get; set; }
        public string? RouterLink { get; set; }
        public bool IsDropdownMenu { get; set; }
        public int? ParentMenuItemId { get; set; }
        public virtual MenuItem? ParentMenuItem { get; set; }
        public virtual ICollection<MenuItem>? SubMenuItems { get; set; }
        public virtual ICollection<MenuItemOperationClaim>? MenuItemOperationClaims { get; set; }
        public bool HasParent => ParentMenuItem is not null;
        public bool HasChildren => SubMenuItems is not null && SubMenuItems.Any();
        public bool HasMenuItemOperationClaims => MenuItemOperationClaims is not null && MenuItemOperationClaims.Any();
    }
}
