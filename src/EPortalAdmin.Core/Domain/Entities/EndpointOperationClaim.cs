namespace EPortalAdmin.Core.Domain.Entities
{
    public class EndpointOperationClaim : BaseEntity
    {
        public int EndpointId { get; set; }
        public virtual Endpoint Endpoint { get; set; }
        public int OperationClaimId { get; set; }
        public virtual OperationClaim OperationClaim { get; set; }
    }
}
