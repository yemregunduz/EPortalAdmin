namespace EPortalAdmin.Core.Logging.Serilog.ConfigurationModels
{
    public class ConsoleLogOptions
    {
        public static readonly string AppSettingsKey = "SeriLogOptions:Providers:Console";
        public string OutputTemplate { get; set; }
        public static ConsoleLogOptions Default => new()
        {
            OutputTemplate = "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level}] {Message}{NewLine}{Exception}"
        };
    }
}
