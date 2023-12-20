namespace EPortalAdmin.Core.FileStorage
{
    public interface IStorageService : IStorage
    {
        public string StorageName { get; }
    }
}
