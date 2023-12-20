using EPortalAdmin.Application;
using EPortalAdmin.Core;
using EPortalAdmin.Core.Utilities.Extensions;
using EPortalAdmin.Core.Utilities.Helpers;
using EPortalAdmin.Persistence;
using EPortalAdmin.WebAPI;
using EPortalAdmin.WebAPI.Controllers;
using Microsoft.AspNetCore.OData;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services
    .AddControllers()
    .AddOData(opt =>
    {
        opt.EnableQueryFeatures();
    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services
    .AddEndpointsApiExplorer()
    .AddWebApiServices()
    .AddPersistenceServices(builder.Configuration)
    .AddApplicationServices()
    .AddCoreServices(builder.Configuration)
    .AddHttpContextAccessor()
    .AddCustomCors(builder.Configuration)
    .AddJwtAuthenticationServices(builder.Configuration);

var app = builder.Build();

HttpContextAccessorSingleton.Configure(app.Services);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app
    .ExploreEndpoints<EPortalAdminDbContext, BaseController>(Assembly.GetExecutingAssembly())
    .UseCustomLoggingMiddleware()
    .UseStaticFiles()
    .UseCors()
    .UseHttpsRedirection()
    .UseCustomExceptionMiddleware()
    .UseAttachUserMiddleware()
    .UseAuthentication()
    .UseAuthorization();

EndpointHelper.SetEndpointList<EPortalAdminDbContext>(app);

app.MapControllers();

app.Run();
