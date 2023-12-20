using EPortalAdmin.Core.Domain;
using EPortalAdmin.Core.Domain.Configurations;
using EPortalAdmin.Core.Domain.Constants;
using EPortalAdmin.Core.Domain.Enums;
using EPortalAdmin.Core.Exceptions;
using EPortalAdmin.Core.FileStorage;
using EPortalAdmin.Core.FileStorage.Azure;
using EPortalAdmin.Core.FileStorage.ConfigurationModels;
using EPortalAdmin.Core.FileStorage.Local;
using EPortalAdmin.Core.Logging.Serilog;
using EPortalAdmin.Core.Logging.Serilog.Logger;
using EPortalAdmin.Core.Mailing;
using EPortalAdmin.Core.Mailing.MailKit;
using EPortalAdmin.Core.Middlewares;
using EPortalAdmin.Core.Security.EmailAuthenticator;
using EPortalAdmin.Core.Security.JWT;
using EPortalAdmin.Core.Security.OtpAuthenticator;
using EPortalAdmin.Core.Security.OtpAuthenticator.OtpNet;
using EPortalAdmin.Core.Utilities.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace EPortalAdmin.Core.Utilities.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddLoggingService(this IServiceCollection services, IConfiguration configuration)
        {
            string defaultProvider = configuration.GetSection("SeriLogOptions:CurrentProvider")?
                                        .Value?.ToString()
                                            ?? LoggingProviders.File;

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
                case LoggingProviders.Optimized:
                    services.AddSingleton<LoggerServiceBase, OptimizedLogger>();
                    break;
                default:
                    throw new BusinessException(SerilogMessages.InvalidDefaultProvider, ExceptionCode.InvalidDefaultProvider);
            }

            return services;
        }
        public static IServiceCollection AddStorageServices(this IServiceCollection services, IConfiguration configuration)
        {
            StorageOptions storageOptions = configuration.GetSection(StorageOptions.AppSettingsKey)
                                                .Get<StorageOptions>()
                                                     ?? StorageOptions.Default;

            switch (storageOptions.CurrentProvider)
            {
                case StorageProviders.Azure:
                    services.AddScoped<IStorage, AzureStorage>();
                    break;
                case StorageProviders.Local:
                    services.AddScoped<IStorage, LocalStorage>();
                    break;
                default:
                    throw new BusinessException(StorageMessages.InvalidDefaultProvider, ExceptionCode.InvalidDefaultProvider);
            }

            services.AddScoped<IStorageService, StorageService>();

            return services;
        }
        public static IServiceCollection AddMailServices(this IServiceCollection services)
        {
            services.AddScoped<IMailService, MailKitMailService>();
            return services;
        }
        public static IServiceCollection AddHelperServices(this IServiceCollection services)
        {
            services.AddScoped<IEmailAuthenticatorHelper, EmailAuthenticatorHelper>();
            services.AddScoped<IOtpAuthenticatorHelper, OtpNetOtpAuthenticatorHelper>();
            services.AddScoped<ITokenHelper, JwtHelper>();
            return services;
        }
        public static IServiceCollection AddCurrentUserServices(this IServiceCollection services)
        {
            services.AddScoped<CurrentUser>();
            return services;
        }
        public static IServiceCollection AddMiddlewareServices(this IServiceCollection services)
        {
            services.AddSingleton<ExceptionMiddleware>();
            services.AddSingleton<LoggingMiddleware>();
            services.AddSingleton<AttachUserMiddleware>();
            return services;
        }
        public static IServiceCollection AddJwtAuthenticationServices(this IServiceCollection services, IConfiguration configuration)
        {
            TokenOptions? tokenOptions = configuration.GetSection(TokenOptions.AppSettingsKey)
                                            .Get<TokenOptions>()
                                                ?? throw new NotFoundException(
                                                    $"\"{TokenOptions.AppSettingsKey}\" section cannot found in configuration.",ExceptionCode.TokenOptionsKeyNotFound);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidIssuer = tokenOptions?.Issuer,
                    ValidAudience = tokenOptions?.Audience,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions?.SecurityKey ?? "")
                };
            });

            return services;
        }

        public static IServiceCollection AddCustomCors(this IServiceCollection services, IConfiguration configuration)
        {
            WebApiConfiguration webApiConfiguration =
                configuration.GetSection(WebApiConfiguration.AppSettingsKey).Get<WebApiConfiguration>()
                    ?? throw new InvalidOperationException($"\"{WebApiConfiguration.AppSettingsKey}\" section cannot found in configuration.");

            services.AddCors(options => options.AddDefaultPolicy(policy =>
                policy.WithOrigins(webApiConfiguration.AllowedOrigins).AllowAnyHeader().AllowAnyMethod().AllowCredentials()
            ));

            return services;
        }
    }
}
