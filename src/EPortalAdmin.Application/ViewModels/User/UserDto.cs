using EPortalAdmin.Application.ViewModels.UserOperationClaim;
using EPortalAdmin.Core.Domain.Enums;

namespace EPortalAdmin.Application.ViewModels.User
{
    public class UserDto : ViewModelBase
    {
        public UserDto()
        {
            UserOperationClaims = new HashSet<UserOperationClaimDto>();
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public AuthenticatorType AuthenticatorType { get; set; }
        public bool Status { get; set; }
        public ICollection<UserOperationClaimDto> UserOperationClaims { get; set; }
    }
}
