using EPortalAdmin.Core.Exceptions;
using EPortalAdmin.Core.Logging;
using EPortalAdmin.Core.Logging.Serilog;
using EPortalAdmin.Core.Utilities.Helpers;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Net;

namespace EPortalAdmin.Core.Middlewares
{
    public class ExceptionMiddleware : IMiddleware
    {
        private readonly Dictionary<Type, Func<HttpContext, Exception, CustomProblemDetails>> exceptionHandlers;
        private readonly LoggerServiceBase _loggerService;

        public ExceptionMiddleware(LoggerServiceBase loggerService)
        {
            exceptionHandlers = new Dictionary<Type, Func<HttpContext, Exception, CustomProblemDetails>>
            {
                { typeof(ValidationException), CreateValidationException },
                { typeof(BusinessException), CreateBusinessException },
                { typeof(NotFoundException), CreateNotFoundException },
                { typeof(AuthorizationException), CreateAuthorizationException }
            };

            _loggerService = loggerService;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception exception)
            {
                await HandleExceptionAsync(context, exception);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            if (exceptionHandlers.TryGetValue(exception.GetType(), out var handler))
            {
                var problemDetails = handler(context, exception);
                return context.Response.WriteAsync(problemDetails.ToString());
            }

            var internalDetails = CreateInternalException(context, exception);
            return context.Response.WriteAsync(internalDetails.ToString());
        }

        private CustomProblemDetails CreateAuthorizationException(HttpContext context, Exception exception)
        {
            return CreateProblemDetails(context, exception, HttpStatusCode.Unauthorized, "https://example.com/probs/authorization", "Authorization exception");
        }

        private CustomProblemDetails CreateBusinessException(HttpContext context, Exception exception)
        {
            return CreateProblemDetails(context, exception, HttpStatusCode.BadRequest, "https://example.com/probs/business", "Business exception");
        }

        private CustomProblemDetails CreateValidationException(HttpContext context, Exception exception)
        {
            var validationException = (ValidationException)exception;
            var errors = validationException.Errors.Select(e => new { e.PropertyName, e.ErrorMessage, e.AttemptedValue });
            return CreateProblemDetails(context, exception, HttpStatusCode.BadRequest, "https://example.com/probs/validation", "Validation error(s)", errors);
        }

        private CustomProblemDetails CreateNotFoundException(HttpContext context, Exception exception)
        {
            return CreateProblemDetails(context, exception, HttpStatusCode.NotFound, "https://example.com/probs/not-found", "Not Found Exception");
        }

        private CustomProblemDetails CreateInternalException(HttpContext context, Exception exception)
        {
            return CreateProblemDetails(context, exception, HttpStatusCode.InternalServerError, "https://example.com/probs/internal", "Internal exception", innerExceptionDetail: exception.InnerException?.Message ?? "");
        }

        private CustomProblemDetails CreateProblemDetails(HttpContext context, Exception exception, HttpStatusCode statusCode, string type, string title, object errors = null, string innerExceptionDetail = null)
        {
            context.Response.StatusCode = Convert.ToInt32(statusCode);

            var problemDetails = new CustomProblemDetails
            {
                Status = (int)statusCode,
                Type = type,
                Title = title,
                Detail = exception.Message,
                Instance = context.Request.Path,
            };

            if (errors != null)
            {
                problemDetails.Extensions["ValidationErrors"] = errors;
            }

            if (!string.IsNullOrEmpty(innerExceptionDetail))
            {
                problemDetails.Extensions["InnerExceptionDetail"] = innerExceptionDetail;
            }

            CreateExceptionLog(context, exception, problemDetails);

            return problemDetails;
        }

        private void CreateExceptionLog(HttpContext context, Exception exception, CustomProblemDetails problemDetails)
        {
            ExceptionLog exceptionLog = LoggingHelper.GetExceptionLog(context, exception, problemDetails);
            SerilogHelpers.PushExceptionLogProperty(exceptionLog);
            _loggerService.Error(JsonConvert.SerializeObject(problemDetails));
        }
    }
}
