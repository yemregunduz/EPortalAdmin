namespace EPortalAdmin.Core.Domain.Dtos
{
    public class UserForChangePasswordDto : UserForLoginDto
    {
        public string NewPassword { get; set; }
    }
}
