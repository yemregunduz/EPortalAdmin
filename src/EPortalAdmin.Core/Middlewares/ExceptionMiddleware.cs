using FluentValidation;
using Microsoft.AspNetCore.Http;
using EPortalAdmin.Core.Exceptions;
using System.Net;

namespace EPortalAdmin.Core.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                await HandleExceptionAsync(context, exception);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            if (exception.GetType() == typeof(ValidationException))
                return CreateValidationException(context, exception);
            if (exception.GetType() == typeof(BusinessException))
                return CreateBusinessException(context, exception);
            if (exception.GetType() == typeof(NotFoundException))
                return CreateNotFoundException(context, exception);
            if (exception.GetType() == typeof(AuthorizationException))
                return CreateAuthorizationException(context, exception);
            return CreateInternalException(context, exception);
        }

        private Task CreateAuthorizationException(HttpContext context, Exception exception)
        {
            context.Response.StatusCode = Convert.ToInt32(HttpStatusCode.Unauthorized);

            return context.Response.WriteAsync(new AuthorizationProblemDetails
            {
                Status = StatusCodes.Status401Unauthorized,
                Type = "https://example.com/probs/authorization",
                Title = "Authorization exception",
                Detail = exception.Message,
                Instance = context.Request.Path
            }.ToString());
        }

        private Task CreateBusinessException(HttpContext context, Exception exception)
        {
            context.Response.StatusCode = Convert.ToInt32(HttpStatusCode.BadRequest);

            return context.Response.WriteAsync(new BusinessProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Type = "https://example.com/probs/business",
                Title = "Business exception",
                Detail = exception.Message,
                Instance = context.Request.Path
            }.ToString());
        }

        private Task CreateValidationException(HttpContext context, Exception exception)
        {
            context.Response.StatusCode = Convert.ToInt32(HttpStatusCode.BadRequest);
            object errors = ((ValidationException)exception).Errors.Select(e => new { e.PropertyName, e.ErrorMessage, e.AttemptedValue });

            return context.Response.WriteAsync(new ValidationProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Type = "https://example.com/probs/validation",
                Title = "Validation error(s)",
                Detail = "",
                Instance = context.Request.Path,
                Errors = errors
            }.ToString());
        }

        private Task CreateNotFoundException(HttpContext context, Exception exception)
        {
            context.Response.StatusCode = Convert.ToInt32(HttpStatusCode.NotFound);

            return context.Response.WriteAsync(new NotFoundProblemDetails
            {
                Status = StatusCodes.Status404NotFound,
                Type = "https://example.com/probs/not-found",
                Title = "Not Found Exception",
                Detail = exception.Message,
                Instance = context.Request.Path,
            }.ToString());
        }

        private Task CreateInternalException(HttpContext context, Exception exception)
        {
            context.Response.StatusCode = Convert.ToInt32(HttpStatusCode.InternalServerError);

            return context.Response.WriteAsync(new InternalProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Type = "https://example.com/probs/internal",
                Title = "Internal exception",
                Detail = exception.Message,
                InnerExceptionDetail = exception.InnerException?.Message ?? "",
                Instance = context.Request.Path
            }.ToString());
        }
    }
}

