using EPortalAdmin.Application.Repositories;
using EPortalAdmin.Core.Domain.Entities;
using EPortalAdmin.Core.Persistence.Repositories;
using EPortalAdmin.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EPortalAdmin.Persistence
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<EPortalAdminDbContext>(options =>
                                         options.UseSqlServer(
                                             configuration.GetConnectionString("EPortalAdminConnectionString")));

            #region Repository Dependencies
            services.AddScoped<IEmailAuthenticatorRepository, EmailAuthenticatorRepository>();
            services.AddScoped<IEndpointRepository, EndpointRepository>();
            services.AddScoped<IEndpointOperationClaimRepository, EndpointOperationClaimRepository>();
            services.AddScoped<IFileRepository, FileRepository>();
            services.AddScoped<IMenuItemRepository, MenuItemRepository>();
            services.AddScoped<IMenuItemOperationClaimRepository, MenuItemOperationClaimRepository>();
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IOperationClaimRepository, OperationClaimRepository>();
            services.AddScoped<IOtpAuthenticatorRepository, OtpAuthenticatorRepository>();
            services.AddScoped<IUserOperationClaimRepository, UserOperationClaimRepository>();


            services.AddScoped<IRepository<EmailAuthenticator>, EmailAuthenticatorRepository>();
            services.AddScoped<IRepository<Endpoint>, EndpointRepository>();
            services.AddScoped<IRepository<EndpointOperationClaim>, EndpointOperationClaimRepository>();
            services.AddScoped<IRepository<Core.FileStorage.File>, FileRepository>();
            services.AddScoped<IRepository<MenuItem>, MenuItemRepository>();
            services.AddScoped<IRepository<MenuItemOperationClaim>, MenuItemOperationClaimRepository>();
            services.AddScoped<IRepository<RefreshToken>, RefreshTokenRepository>();
            services.AddScoped<IRepository<OperationClaim>, OperationClaimRepository>();
            services.AddScoped<IRepository<OtpAuthenticator>, OtpAuthenticatorRepository>();
            services.AddScoped<IRepository<User>, UserRepository>();
            services.AddScoped<IRepository<UserOperationClaim>, UserOperationClaimRepository>();
            #endregion

            return services;
        }
    }
}
