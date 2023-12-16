namespace EPortalAdmin.Core.Domain.Entities
{
    public class BaseEntity : AuditLogEntity, ISoftDelete
    {
        public int Id { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public virtual DateTime? UpdatedDate { get; set; }
        public bool IsDeleted { get; set; } = false;
        public int? DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
        public void MarkAsDelete(int userId)
        {
            IsDeleted = true;
            DeletedDate = DateTime.UtcNow;
            DeletedBy = userId;
        }
    }
    public interface ISoftDelete
    {
        public bool IsDeleted { get; set; }
        public int? DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
    public abstract class AuditLogEntity
    {
        public int? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
