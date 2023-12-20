using EPortalAdmin.Core.Attributes;
using EPortalAdmin.Core.Domain.Entities;
using EPortalAdmin.Core.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace EPortalAdmin.Core.Utilities.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseCustomExceptionMiddleware(this IApplicationBuilder app)
            => app.UseMiddleware<ExceptionMiddleware>();

        public static IApplicationBuilder UseCustomLoggingMiddleware(this IApplicationBuilder app)
            => app.UseMiddleware<LoggingMiddleware>();

        public static IApplicationBuilder UseAttachUserMiddleware(this IApplicationBuilder app)
            => app.UseMiddleware<AttachUserMiddleware>();

        public static IApplicationBuilder ExploreEndpoints<T, TController>(this IApplicationBuilder app, Assembly assembly)
            where T : DbContext
            where TController : ControllerBase
        {
            using var scope = app.ApplicationServices.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<T>();
            var controllerTypes = assembly.GetTypes()
                                        .Where(t => t.IsAssignableTo(typeof(TController)));

            foreach (var controllerType in controllerTypes)
            {
                foreach (MethodInfo controllerMethod in controllerType.GetMethods())
                {
                    var explorableEndpointAttribute = controllerMethod.GetCustomAttribute<ExplorableEndpointAttribute>();
                    if (explorableEndpointAttribute is not null)
                    {
                        var table = dbContext.Set<Endpoint>();
                        var existingEndpoint = table
                            .FirstOrDefault(e => e.Controller.Equals(controllerType.Name) 
                            && e.Action.Equals(controllerMethod.Name));

                        if(existingEndpoint is not null && 
                            !existingEndpoint.Description.Equals(explorableEndpointAttribute.Description)
                            )
                        {
                            existingEndpoint.Description = explorableEndpointAttribute.Description;
                            existingEndpoint.UpdatedDate = DateTime.UtcNow;
                        }

                        if (existingEndpoint is null)
                        {
                            var explorableEndpoint = new Endpoint
                            {
                                CreatedDate = DateTime.UtcNow,  
                                Controller = controllerType.Name,
                                Action = controllerMethod.Name,
                                Description = explorableEndpointAttribute.Description
                            };
                            table.Add(explorableEndpoint);

                        }
                    }
                }
            }
            dbContext.SaveChanges();
            return app;
        }
    }
}
