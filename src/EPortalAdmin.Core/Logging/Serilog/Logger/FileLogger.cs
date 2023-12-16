using System;
using System.IO;
using EPortalAdmin.Core.Logging.Serilog.ConfigurationModels;
using EPortalAdmin.Core.Logging.Serilog;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Context;

namespace MrPeinir.Core.Logging.Serilog.Logger
{
    public class FileLogger : LoggerServiceBase
    {
        private readonly IConfiguration _configuration;

        public FileLogger(IConfiguration configuration)
        {
            _configuration = configuration;

            FileLogOptions logConfig = _configuration.GetSection(FileLogOptions.AppSettingsKey)
                                                        .Get<FileLogOptions>()
                                                            ?? throw new NullReferenceException(SerilogMessages.NullOptionsMessage);

            string logFilePath = string.Format("{0}{1}", Directory.GetCurrentDirectory() + $"\\{logConfig.FolderPath}\\", ".txt");

            Logger = new LoggerConfiguration()
                     .WriteTo.Logger(lc => lc
                        .Filter.ByIncludingOnly(evt => evt.Properties.ContainsKey("LogType") &&
                                                      evt.Properties["LogType"].ToString() == "FileLog")
                        .WriteTo.File(
                            path: logFilePath,
                            rollingInterval: RollingInterval.Day,
                            retainedFileCountLimit: logConfig.RetainedFileCountLimit,
                            fileSizeLimitBytes: logConfig.FileSizeLimitBytes,
                            outputTemplate: logConfig.OutputTemplate))
                     .CreateLogger();

            LogContext.PushProperty("LogType", "FileLog");
        }
    }
}
