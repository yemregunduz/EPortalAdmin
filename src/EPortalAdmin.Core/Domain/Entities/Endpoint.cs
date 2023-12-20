namespace EPortalAdmin.Core.Domain.Entities
{
    public class Endpoint : BaseEntity
    {
        public string Controller { get; set; }
        public string Action { get; set; }
        public string Description { get; set; }
    }
}
