using EPortalAdmin.Core.Domain.Constants;
using EPortalAdmin.Core.Exceptions;
using EPortalAdmin.Core.Logging.Serilog;
using EPortalAdmin.Core.Logging.Serilog.Logger;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EPortalAdmin.Core
{
    public static class CoreServiceRegistration
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddLoggingService(configuration);
            return services;
        }

        public static IServiceCollection AddLoggingService(this IServiceCollection services, IConfiguration configuration)
        {
            string defaultProvider = configuration.GetSection("SeriLogOptions:CurrentProvider")?.Value?.ToString()
                ?? throw new NotFoundException(SerilogMessages.NullDefaultProvider);

            switch (defaultProvider)
            {
                case LoggingProviders.MSSql:
                    services.AddSingleton<LoggerServiceBase, MsSqlLogger>();
                    break;

                case LoggingProviders.File:
                    services.AddSingleton<LoggerServiceBase, FileLogger>();
                    break;

                case LoggingProviders.Console:
                    services.AddSingleton<LoggerServiceBase, ConsoleLogger>();
                    break;

                case LoggingProviders.ElasticSearch:
                    services.AddSingleton<LoggerServiceBase, ElasticSearchLogger>();
                    break;

                default:
                    throw new BusinessException(SerilogMessages.InvalidDefaultProvider);
            }

            return services;
        }
    }
}
