using EPortalAdmin.Core.Exceptions;
using EPortalAdmin.Core.Utilities.Extensions;
using EPortalAdmin.Core.Utilities.Extensions.Claims;
using EPortalAdmin.Domain.Constants;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace EPortalAdmin.Application.Pipelines.Authorization
{
    public class AuthorizationBehavior<TRequest, TResponse>(IHttpContextAccessor httpContextAccessor) : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>, ISecuredRequest
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            List<string>? roleClaims = httpContextAccessor.HttpContext.User.ClaimRoles() ??
                throw new AuthorizationException(Messages.Authorization.ClaimsNotFound);

            var controllerName = httpContextAccessor.HttpContext.GetCurrentController();
            var actionName = httpContextAccessor.HttpContext.GetCurrentAction();


            //bool isAuthorized =
            //    request.Roles is null || !request.Roles.Any() || request.Roles.Any(role => roleClaims?.Contains(role) == true);

            //if (!isAuthorized)
            //    throw new AuthorizationException(Messages.Authorization.NotAuthorized);

            TResponse response = await next();
            return response;
        }
    }
}
