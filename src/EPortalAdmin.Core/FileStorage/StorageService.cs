using Microsoft.AspNetCore.Http;

namespace EPortalAdmin.Core.FileStorage
{
    public class StorageService : IStorageService
    {
        readonly IStorage _storage;

        public StorageService(IStorage storage)
        {
            _storage = storage;
        }

        public string StorageName { get => _storage.GetType().Name; }

        public async Task DeleteAsync(string pathOrContainerName, string fileName)
            => await _storage.DeleteAsync(pathOrContainerName, fileName);

        public IList<string> GetFiles(string pathOrContainerName)
            => _storage.GetFiles(pathOrContainerName);

        public bool HasFile(string pathOrContainerName, string fileName)
            => _storage.HasFile(pathOrContainerName, fileName);

        public Task<IList<(string fileName, string pathOrContainerName)>> BulkUploadAsync(string pathOrContainerName, IFormFileCollection files)
            => _storage.BulkUploadAsync(pathOrContainerName, files);

        public async Task<(string fileName, string pathOrContainerName)> UploadAsync(string pathOrContainerName, IFormFile file)
            => await _storage.UploadAsync(pathOrContainerName, file);

    }
}
