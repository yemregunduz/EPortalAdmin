namespace EPortalAdmin.Core.FileStorage.ConfigurationModels
{
    public class AzureStorageOptions
    {
        public static readonly string AppSettingsKey = "StorageOptions:Azure";
        public string ConnectionString { get; set; }
        public static AzureStorageOptions Current => new AzureStorageOptions
        {
            ConnectionString = "UseDevelopmentStorage=true"
        };
    }
}
