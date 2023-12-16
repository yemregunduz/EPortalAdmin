namespace EPortalAdmin.Core.Domain.Entities
{
    public class OperationClaim : BaseEntity
    {
        public string Name { get; set; }
        public OperationClaim()
        {
        }
        public OperationClaim(string name)
        {
            Name = name;
        }
    }
}
