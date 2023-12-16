using EPortalAdmin.Core.Logging.Serilog.ConfigurationModels;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Events;

namespace EPortalAdmin.Core.Logging.Serilog.Logger
{
    public class ConsoleLogger : LoggerServiceBase
    {
        public ConsoleLogger(IConfiguration configuration)
        {
            ConsoleLogOptions logConfig = configuration.GetSection(ConsoleLogOptions.AppSettingsKey)
                                                                .Get<ConsoleLogOptions>()
                                                                     ?? ConsoleLogOptions.Default;

            Logger = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .WriteTo.Console(
                    outputTemplate: logConfig.OutputTemplate,
                    restrictedToMinimumLevel: LogEventLevel.Information)
                .CreateLogger();
        }
    }
}
