using EPortalAdmin.Core.Utilities.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EPortalAdmin.Core
{
    public static class CoreServiceRegistration
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddLoggingService(configuration);
            services.AddStorageServices(configuration);
            services.AddMailServices();
            services.AddHelperServices();
            services.AddCurrentUserServices();
            services.AddMiddlewareServices();
            return services;
        }
    }
}
