namespace EPortalAdmin.Core.Domain.Entities
{
    public class MenuItemOperationClaim : BaseEntity
    {
        public int MenuItemId { get; set; }
        public int OperationClaimId { get; set; }
        public virtual MenuItem MenuItem { get; set; }
        public virtual OperationClaim OperationClaim { get; set; }
    }
}
