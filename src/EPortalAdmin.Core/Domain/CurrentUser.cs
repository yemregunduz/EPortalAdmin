namespace EPortalAdmin.Core.Domain
{
    public class CurrentUser
    {
        public int? UserId { get; set; }
        public Guid CorrelationId { get; set; }
        public bool IsAuthenticated => UserId.HasValue;
    }
}
