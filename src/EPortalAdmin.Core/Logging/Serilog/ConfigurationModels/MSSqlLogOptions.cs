namespace EPortalAdmin.Core.Logging.Serilog.ConfigurationModels
{
    public class MSSqlLogOptions
    {
        public static readonly string AppSettingsKey = "SeriLogOptions:Providers:MSSql";
        public string ConnectionString { get; set; }
        public string LogTableName { get; set; }
        public string ExceptionLogTableName { get; set; }
        public bool AutoCreateSqlTable { get; set; }
    }
}
