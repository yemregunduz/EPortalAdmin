using Microsoft.AspNetCore.Builder;

namespace EPortalAdmin.Core.Middlewares
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder ConfigureCustomExceptionMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ExceptionMiddleware>();
        }
        public static IApplicationBuilder ConfigureCustomLoggingMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<LoggingMiddleware>();
        }
    }
}
