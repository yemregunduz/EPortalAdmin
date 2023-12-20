using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using EPortalAdmin.Core.FileStorage.ConfigurationModels;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace EPortalAdmin.Core.FileStorage.Azure
{
    public class AzureStorage : Storage, IAzureStorage
    {
        readonly BlobServiceClient _blobServiceClient;
        BlobContainerClient _blobContainerClient;
        public AzureStorage(IConfiguration configuration)
        {
            AzureStorageOptions azureStorageOptions = configuration.GetSection(AzureStorageOptions.AppSettingsKey)
                                                                        .Get<AzureStorageOptions>()
                                                                            ?? AzureStorageOptions.Current;
            _blobServiceClient = new(azureStorageOptions.ConnectionString);
        }
        public async Task DeleteAsync(string containerName, string fileName)
        {
            _blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            BlobClient blobClient = _blobContainerClient.GetBlobClient(fileName);
            await blobClient.DeleteAsync();
        }

        public IList<string> GetFiles(string containerName)
        {
            _blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            return _blobContainerClient.GetBlobs().Select(b => b.Name).ToList();
        }

        public bool HasFile(string containerName, string fileName)
        {
            _blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            return _blobContainerClient.GetBlobs().Any(b => b.Name == fileName);
        }

        public async Task<IList<(string fileName, string pathOrContainerName)>> BulkUploadAsync(string containerName, IFormFileCollection files)
        {
            _blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            await _blobContainerClient.CreateIfNotExistsAsync();
            await _blobContainerClient.SetAccessPolicyAsync(PublicAccessType.BlobContainer);

            List<(string fileName, string pathOrContainerName)> datas = new();
            foreach (IFormFile file in files)
            {
                var data = await UploadAsync(containerName, file);
                datas.Add(data);
            }
            return datas;
        }

        public async Task<(string fileName, string pathOrContainerName)> UploadAsync(string containerName, IFormFile file)
        {
            _blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            await _blobContainerClient.CreateIfNotExistsAsync();
            await _blobContainerClient.SetAccessPolicyAsync(PublicAccessType.BlobContainer);

            string fileNewName = await FileRenameAsync(containerName, file.Name, HasFile);

            BlobClient blobClient = _blobContainerClient.GetBlobClient(fileNewName);
            await blobClient.UploadAsync(file.OpenReadStream());

            return (fileNewName, $"{containerName}/{fileNewName}");
        }

    }
}
