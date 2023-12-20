namespace EPortalAdmin.Application.ViewModels.Endpoint
{
    public class EndpointDto : ViewModelBase
    {
        public string Controller { get; set; }
        public string Action { get; set; }
        public string Description { get; set; }
    }
}
