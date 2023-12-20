using EPortalAdmin.Core.Logging.Serilog.ConfigurationModels;
using EPortalAdmin.Core.Utilities.Helpers;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Events;

namespace EPortalAdmin.Core.Logging.Serilog.Logger
{
    public class OptimizedLogger : LoggerServiceBase
    {
        public OptimizedLogger(IConfiguration configuration)
        {
            MSSqlLogOptions msSqlLogOptions = configuration.GetSection(MSSqlLogOptions.AppSettingsKey)
                                                                .Get<MSSqlLogOptions>()
                                                                    ?? throw new NullReferenceException(SerilogMessages.NullOptionsMessage);

            FileLogOptions fileLogOptions = configuration.GetSection(FileLogOptions.AppSettingsKey)
                                                                .Get<FileLogOptions>()
                                                                    ?? FileLogOptions.Default;

            string logFilePath = string.Format("{0}{1}", Directory.GetCurrentDirectory() + $"\\{fileLogOptions.FolderPath}\\", ".txt");

            Logger = new LoggerConfiguration()
                    .Enrich.FromLogContext()
                    .WriteTo.Logger(lc => lc
                        .Filter.ByIncludingOnly(e => e.Level < LogEventLevel.Error)
                        .WriteTo.File(
                         path: logFilePath,
                         rollingInterval: RollingInterval.Day,
                         retainedFileCountLimit: fileLogOptions.RetainedFileCountLimit,
                         fileSizeLimitBytes: fileLogOptions.FileSizeLimitBytes,
                         outputTemplate: fileLogOptions.OutputTemplate)
                    .WriteTo.Logger(lc => lc
                        .Filter.ByIncludingOnly(e => e.Level >= LogEventLevel.Error)
                        .WriteTo.MSSqlServer(
                            connectionString: msSqlLogOptions.ConnectionString,
                            tableName: msSqlLogOptions.ExceptionLogTableName,
                            autoCreateSqlTable: msSqlLogOptions.AutoCreateSqlTable,
                            columnOptions: SerilogHelpers.GetExceptionLogTableColumnOptions())))
                    .CreateLogger();
        }

    }
}
