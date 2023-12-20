namespace EPortalAdmin.Core.FileStorage.ConfigurationModels
{
    public class StorageOptions
    {
        public static readonly string AppSettingsKey = "StorageOptions";
        public string CurrentProvider { get; set; }
        public static StorageOptions Default => new StorageOptions
        {
            CurrentProvider = "Local"
        };
    }
}
