namespace EPortalAdmin.Application.ViewModels.OtpAuthenticator
{
    public class OtpAuthenticatorDto
    {
        public string SecretKey { get; set; }

        public OtpAuthenticatorDto()
        {
            SecretKey = string.Empty;
        }

        public OtpAuthenticatorDto(string secretKey)
        {
            SecretKey = secretKey;
        }
    }
}
