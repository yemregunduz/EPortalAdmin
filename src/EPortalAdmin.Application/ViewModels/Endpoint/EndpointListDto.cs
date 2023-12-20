using EPortalAdmin.Core.Persistence.Paging;

namespace EPortalAdmin.Application.ViewModels.Endpoint
{
    public class EndpointListDto : BasePageableModel
    {
        public IList<EndpointDto> Items { get; set; }
    }
}
