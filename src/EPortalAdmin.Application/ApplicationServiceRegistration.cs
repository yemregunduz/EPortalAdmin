using EPortalAdmin.Application.Features.Authorizations.Rules;
using EPortalAdmin.Application.Features.EndpointOperationClaims.Rules;
using EPortalAdmin.Application.Features.Endpoints.Rules;
using EPortalAdmin.Application.Features.OperationClaims.Rules;
using EPortalAdmin.Application.Features.UserOperationClaims.Rules;
using EPortalAdmin.Application.Features.Users.Rules;
using EPortalAdmin.Application.Pipelines.Authorization;
using EPortalAdmin.Application.Pipelines.Caching;
using EPortalAdmin.Application.Pipelines.Performance;
using EPortalAdmin.Application.Pipelines.Transaction;
using EPortalAdmin.Application.Pipelines.Validation;
using EPortalAdmin.Application.Services.AuthenticatorService;
using EPortalAdmin.Application.Services.AuthService;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace EPortalAdmin.Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                cfg.AddOpenBehavior(typeof(AuthorizationBehavior<,>));
                cfg.AddOpenBehavior(typeof(CachingBehavior<,>));
                cfg.AddOpenBehavior(typeof(CacheRemovingBehavior<,>));
                cfg.AddOpenBehavior(typeof(TransactionalBehavior<,>));
                cfg.AddOpenBehavior(typeof(PerformanceBehavior<,>));
                cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
            });
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            services.AddDistributedMemoryCache();

            #region Business Rules Dependencies
            services.AddScoped<AuthorizationBusinessRules>();
            services.AddScoped<EndpointBusinessRules>();
            services.AddScoped<EndpointOperationClaimBusinessRules>();
            services.AddScoped<OperationClaimBusinessRules>();
            services.AddScoped<UserBusinessRules>();
            services.AddScoped<UserOperationClaimBusinessRules>();
            #endregion

            #region Manager Dependencies
            services.AddScoped<IAuthService, AuthManager>();
            services.AddScoped<IAuthenticatorService, AuthenticatorManager>();
            #endregion

            return services;
        }
    }
}
