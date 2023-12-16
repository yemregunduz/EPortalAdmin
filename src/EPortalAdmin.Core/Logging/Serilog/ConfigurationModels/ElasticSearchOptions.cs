namespace EPortalAdmin.Core.Logging.Serilog.ConfigurationModels
{
    public class ElasticSearchOptions
    {
        public static readonly string AppSettingsKey = "SeriLogOptions:Providers:ElasticSearch";
        public string ConnectionString { get; set; }
    }
}
