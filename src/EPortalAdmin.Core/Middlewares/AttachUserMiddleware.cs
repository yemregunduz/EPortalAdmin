using EPortalAdmin.Core.Domain;
using EPortalAdmin.Core.Utilities.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace EPortalAdmin.Core.Middlewares
{
    public class AttachUserMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            CurrentUser? currentUser = context.RequestServices.GetRequiredService<CurrentUser>();

            if (currentUser is not null)
            {
                currentUser.UserId = context.GetCurrentUserId();
                currentUser.CorrelationId = Guid.NewGuid();
            }

            await next(context);
        }
    }
}
