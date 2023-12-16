using MediatR;
using Microsoft.AspNetCore.Http;
using EPortalAdmin.Core.Exceptions;
using EPortalAdmin.Domain.Constants;
using EPortalAdmin.Core.Utilities.Extensions.Claims;

namespace EPortalAdmin.Application.Pipelines.Authorization
{
    public class AuthorizationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>, ISecuredRequest
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthorizationBehavior(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            List<string>? roleClaims = _httpContextAccessor.HttpContext.User.ClaimRoles() ??
                throw new AuthorizationException(Messages.Authorization.ClaimsNotFound);

            bool isAuthorized =
                request.Roles is null || !request.Roles.Any() || request.Roles.Any(role => roleClaims?.Contains(role) == true);

            if (!isAuthorized)
                throw new AuthorizationException(Messages.Authorization.NotAuthorized);

            TResponse response = await next();
            return response;
        }
    }
}
