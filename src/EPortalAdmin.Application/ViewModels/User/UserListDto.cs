using EPortalAdmin.Core.Persistence.Paging;

namespace EPortalAdmin.Application.ViewModels.User
{
    public class UserListDto : BasePageableModel
    {
        public IList<UserDto> Items { get; set; }
    }
}
