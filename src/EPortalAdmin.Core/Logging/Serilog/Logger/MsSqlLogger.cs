using EPortalAdmin.Core.Logging.Serilog.ConfigurationModels;
using EPortalAdmin.Core.Utilities.Helpers;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Events;

namespace EPortalAdmin.Core.Logging.Serilog.Logger
{
    public class MsSqlLogger : LoggerServiceBase
    {
        public MsSqlLogger(IConfiguration configuration)
        {
            MSSqlLogOptions logConfiguration = configuration.GetSection(MSSqlLogOptions.AppSettingsKey)
                                                                .Get<MSSqlLogOptions>()
                                                                    ?? throw new NullReferenceException(SerilogMessages.NullOptionsMessage);

            Logger = new LoggerConfiguration()
                    .Enrich.FromLogContext()
                    .WriteTo.Logger(lc => lc
                        .Filter.ByIncludingOnly(e => e.Level < LogEventLevel.Error)
                        .WriteTo.MSSqlServer(
                            connectionString: logConfiguration.ConnectionString,
                            tableName: logConfiguration.LogTableName,
                            autoCreateSqlTable: logConfiguration.AutoCreateSqlTable,
                            columnOptions: SerilogHelpers.GetLogTableColumnOptions()))
                    .WriteTo.Logger(lc => lc
                        .Filter.ByIncludingOnly(e => e.Level >= LogEventLevel.Error)
                        .WriteTo.MSSqlServer(
                            connectionString: logConfiguration.ConnectionString,
                            tableName: logConfiguration.ExceptionLogTableName,
                            autoCreateSqlTable: logConfiguration.AutoCreateSqlTable,
                            columnOptions: SerilogHelpers.GetExceptionLogTableColumnOptions()))
                    .CreateLogger();
        }

    }
}
