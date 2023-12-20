namespace EPortalAdmin.Application.ViewModels.Authorization
{
    public class RevokedTokenDto
    {
        public int Id { get; set; }
        public string Token { get; set; }
        public RevokedTokenDto()
        {
            Token = string.Empty;
        }
        public RevokedTokenDto(int id, string token)
        {
            Id = id;
            Token = token;
        }
    }
}
