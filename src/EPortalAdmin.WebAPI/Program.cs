using EPortalAdmin.WebAPI;
using EPortalAdmin.Core.Middlewares;
using EPortalAdmin.Core;

var builder = WebApplication.CreateBuilder(args);

WebApiConfiguration webApiConfiguration =
    builder.Configuration.GetSection(WebApiConfiguration.AppSettingsKey).Get<WebApiConfiguration>()
        ?? throw new InvalidOperationException($"\"{WebApiConfiguration.AppSettingsKey}\" section cannot found in configuration.");

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddCoreServices(builder.Configuration)
    .AddCors(options => options.AddDefaultPolicy(policy =>
        policy.WithOrigins(webApiConfiguration.AllowedOrigins).AllowAnyHeader().AllowAnyMethod().AllowCredentials()
    ));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.ConfigureCustomLoggingMiddleware()
    .UseStaticFiles()
    .UseCors()
    .UseHttpsRedirection()
    .ConfigureCustomExceptionMiddleware()
    .UseAuthentication()
    .UseAuthorization();

app.MapControllers();

app.Run();
