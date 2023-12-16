using EPortalAdmin.Core.Logging.Serilog.ConfigurationModels;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Sinks.MSSqlServer;

namespace EPortalAdmin.Core.Logging.Serilog.Logger
{
    public class MsSqlLogger : LoggerServiceBase
    {
        public MsSqlLogger(IConfiguration configuration)
        {
            configuration = configuration;

            MSSqlLogOptions logConfiguration = configuration.GetSection(MSSqlLogOptions.AppSettingsKey)
                                                                .Get<MSSqlLogOptions>()
                                                                    ?? throw new NullReferenceException(SerilogMessages.NullOptionsMessage);

            Logger = new LoggerConfiguration()
                .WriteTo.MSSqlServer(
                    connectionString: logConfiguration.ConnectionString,
                    tableName: logConfiguration.LogTableName,
                    autoCreateSqlTable: logConfiguration.AutoCreateSqlTable,
                    columnOptions: GetSqlColumnOptions())
                .CreateLogger();
        }

        private ColumnOptions GetSqlColumnOptions()
        {

            var columnOptions = new ColumnOptions();
            columnOptions.Store.Remove(StandardColumn.Properties);
            return columnOptions;
        }
    }
}
