namespace EPortalAdmin.Core.Logging.Serilog.ConfigurationModels
{
    public class FileLogOptions
    {
        public static readonly string AppSettingsKey = "SeriLogOptions:Providers:File";
        public string FolderPath { get; set; }
        public string OutputTemplate { get; set; }
        public long FileSizeLimitBytes { get; set; }
        public int? RetainedFileCountLimit { get; set; }

        public static FileLogOptions Default => new()
        {
            FileSizeLimitBytes = 52428800,
            FolderPath = "logs",
            OutputTemplate = "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level}] {Message}{NewLine}{Exception}",
            RetainedFileCountLimit = null
        };
    }
}
