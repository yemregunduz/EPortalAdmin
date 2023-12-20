namespace EPortalAdmin.Core.Domain.Configurations
{
    public class WebApiConfiguration
    {
        public readonly static string AppSettingsKey = "WebAPIConfiguration";
        public string ApiDomain { get; set; }
        public string[] AllowedOrigins { get; set; }

        public WebApiConfiguration()
        {
            ApiDomain = string.Empty;
            AllowedOrigins = Array.Empty<string>();
        }

        public WebApiConfiguration(string apiDomain, string[] allowedOrigins)
        {
            ApiDomain = apiDomain;
            AllowedOrigins = allowedOrigins;
        }
    }
}
