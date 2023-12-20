using EPortalAdmin.Core.Domain.Entities;

namespace EPortalAdmin.Core.Utilities.Helpers
{
    public class MenuHelper
    {
        public static IList<MenuItem> CreateMenuTree(IList<MenuItem> menuItems)
        {
            IList<MenuItem> rootMenuItems = new List<MenuItem>();

            foreach (var menuItem in menuItems)
            {
                if (menuItem.ParentMenuItemId == null)
                {
                    rootMenuItems.Add(menuItem);
                }
                else
                {
                    var parentMenuItem = menuItems.FirstOrDefault(m => m.Id == menuItem.ParentMenuItemId);
                    if (parentMenuItem != null)
                    {
                        parentMenuItem.SubMenuItems ??= new List<MenuItem>();
                        parentMenuItem.SubMenuItems.Add(menuItem);
                    }
                }
            }

            return rootMenuItems;
        }
    }
}
