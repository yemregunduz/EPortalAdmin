using EPortalAdmin.Core.Domain;
using EPortalAdmin.Core.Exceptions;
using EPortalAdmin.Core.Logging;
using EPortalAdmin.Core.Utilities.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Text;

namespace EPortalAdmin.Core.Utilities.Helpers
{
    public static class LoggingHelper
    {
        public static LogDetail GetLogDetail(HttpContext context, string requestBody,
            string responseBody, long elapsedResponseTimeInMilliseconds)
            => BuildLogDetail<LogDetail>(context, requestBody, responseBody, elapsedResponseTimeInMilliseconds);

        public static ExceptionLog GetExceptionLog(HttpContext context, Exception exception, CustomProblemDetails problemDetails)
            => BuildExceptionLog<ExceptionLog>(context, exception, problemDetails);

        public static string GetLogMessage(HttpContext context, string requestBody, string responseBody,
            long elapsedResponseTimeInMilliseconds)
        {
            var logMessage = new StringBuilder();

            logMessage
                .AppendLine($"Request Body: {requestBody}")
                .AppendLine($"Request Headers: {context.GetCurrentRequestHeaders()}")
                .AppendLine($"Request Route Values: {context.GetCurrentRouteValues()}")
                .AppendLine($"Request IP Address: {context.GetCurrentIpAddress()}")
                .AppendLine($"Request Method: {context.GetCurrentHttpMethod()}")
                .AppendLine($"Request Query String: {context.GetCurrentQueryString()}")
                .AppendLine($"Request Controller: {context.GetCurrentController()}")
                .AppendLine($"Request Action: {context.GetCurrentAction()}")
                .AppendLine($"Request User Agent: {context.GetCurrentUserAgent()}")
                .AppendLine($"Response Status Code: {context.GetCurrentResponseStatusCode()}")
                .AppendLine($"Response Body: {responseBody}")
                .AppendLine($"Response Elapsed Time: {elapsedResponseTimeInMilliseconds}");

            return logMessage.ToString();
        }

        private static T BuildLogDetail<T>(HttpContext context, string requestBody,
            string responseBody, long elapsedResponseTimeInMilliseconds) where T : LogDetail, new()
        {
            CurrentUser? currentUser = context.RequestServices.GetRequiredService<CurrentUser>();

            var logDetail = new T
            {
                Action = context.GetCurrentAction(),
                Controller = context.GetCurrentController(),
                IpAddress = context.GetCurrentIpAddress(),
                HttpMethod = context.GetCurrentHttpMethod(),
                ResponseHttpStatusCode = context.GetCurrentResponseStatusCode(),
                QueryString = context.GetCurrentQueryString(),
                BrowserName = context.GetCurrentUserAgent(),
                HttpHeaders = context.GetCurrentRequestHeaders(),
                RouteValuesJson = context.GetCurrentRouteValues(),
                UserId = context.GetCurrentUserId(),
                RequestBody = requestBody,
                ResponseBody = responseBody,
                ResponseTimeInMilliseconds = elapsedResponseTimeInMilliseconds,
                CorrelationId = currentUser?.CorrelationId ?? Guid.Empty,
                EndpointId = EndpointHelper.GetId(context.GetCurrentController(), context.GetCurrentAction())
            };

            return logDetail;
        }

        private static T BuildExceptionLog<T>(HttpContext context, Exception exception, CustomProblemDetails problemDetails) where T : ExceptionLog, new()
        {
            var currentUser = context.RequestServices.GetRequiredService<CurrentUser>();

            var exceptionLog = new T
            {
                Title = problemDetails.Title,
                ExceptionMessage = problemDetails.Detail,
                InnerExceptionMessage = problemDetails.Extensions.TryGetValue("InnerExceptionDetail", out var innerExceptionDetail)
                    ? innerExceptionDetail?.ToString() ?? string.Empty
                    : string.Empty,
                HttpStatusCode = problemDetails.Status.Value,
                Instance = problemDetails.Instance,
                Type = exception.GetType().Name,
                CorrelationId = currentUser?.CorrelationId ?? Guid.Empty,
                StackTrace = exception.StackTrace ?? "",
                ValidationErrors = problemDetails.Extensions.TryGetValue("ValidationErrors", out var validationErrors)
                    ? validationErrors?.ToString() ?? string.Empty
                    : string.Empty
            };

            return exceptionLog;
        }
    }
}
