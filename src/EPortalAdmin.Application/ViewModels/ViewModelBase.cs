namespace EPortalAdmin.Application.ViewModels
{
    public class ViewModelBase
    {
        public int Id { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public virtual DateTime? UpdatedDate { get; set; }
        public bool IsDeleted { get; set; } = false;
        public int? DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}
