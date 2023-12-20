using EPortalAdmin.Core.Logging.Serilog.ConfigurationModels;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace EPortalAdmin.Core.Logging.Serilog.Logger
{
    public class FileLogger : LoggerServiceBase
    {
        private readonly IConfiguration _configuration;

        public FileLogger(IConfiguration configuration)
        {
            _configuration = configuration;

            FileLogOptions logConfig = _configuration.GetSection(FileLogOptions.AppSettingsKey)
                                                        .Get<FileLogOptions>()
                                                            ?? FileLogOptions.Default;

            string logFilePath = string.Format("{0}{1}", Directory.GetCurrentDirectory() + $"\\{logConfig.FolderPath}\\", ".txt");

            Logger = new LoggerConfiguration()
                     .WriteTo.File(
                         path: logFilePath,
                         rollingInterval: RollingInterval.Day,
                         retainedFileCountLimit: logConfig.RetainedFileCountLimit,
                         fileSizeLimitBytes: logConfig.FileSizeLimitBytes,
                         outputTemplate: logConfig.OutputTemplate)
                     .CreateLogger();
        }
    }
}
