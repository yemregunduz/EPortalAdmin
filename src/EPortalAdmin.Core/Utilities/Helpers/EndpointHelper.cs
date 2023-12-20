using EPortalAdmin.Core.Domain.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EPortalAdmin.Core.Utilities.Helpers
{
    public static class EndpointHelper
    {
        private static Lazy<List<Endpoint>> _endpoints = new(() => new List<Endpoint>());
        private static readonly object _lock = new();

        public static List<Endpoint> Endpoints => _endpoints.Value;

        public static IApplicationBuilder SetEndpointList<TContext>(IApplicationBuilder app)
            where TContext : DbContext
        {
            lock (_lock)
            {
                if (!_endpoints.IsValueCreated)
                {
                    using var scope = app.ApplicationServices.CreateScope();
                    var dbContext = scope.ServiceProvider.GetRequiredService<TContext>();
                    var endpoints = dbContext.Set<Endpoint>().AsNoTracking();
                    _endpoints.Value.AddRange(endpoints);
                }
            }

            return app;
        }


        public static int GetId(string controller, string action) =>
            _endpoints.Value.Capacity > 0 ? _endpoints.Value.FirstOrDefault(
                    e => e.Controller.Equals(FormatControllerName(controller), StringComparison.OrdinalIgnoreCase) 
                    && e.Action.Equals(action, StringComparison.OrdinalIgnoreCase))
                    ?.Id ?? 0
                : 0;

        public static string FormatControllerName(string controller)
        {
            return controller.EndsWith("Controller") ? controller : $"{controller}Controller";
        }   
    }
}
