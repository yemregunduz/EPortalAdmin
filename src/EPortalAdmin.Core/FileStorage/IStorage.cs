using Microsoft.AspNetCore.Http;

namespace EPortalAdmin.Core.FileStorage
{
    public interface IStorage
    {
        Task<IList<(string fileName, string pathOrContainerName)>> BulkUploadAsync(string pathOrContainerName, IFormFileCollection files);
        Task<(string fileName, string pathOrContainerName)> UploadAsync(string pathOrContainerName, IFormFile file);
        Task DeleteAsync(string pathOrContainerName, string fileName);
        IList<string> GetFiles(string pathOrContainerName);
        bool HasFile(string pathOrContainerName, string fileName);
    }
}
