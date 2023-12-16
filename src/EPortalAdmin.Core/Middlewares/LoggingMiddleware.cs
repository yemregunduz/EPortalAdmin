using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using EPortalAdmin.Core.Logging;
using EPortalAdmin.Core.Logging.Serilog;
using Newtonsoft.Json;
using System.Diagnostics;
using EPortalAdmin.Core.Utilities.Extensions.Claims;

namespace EPortalAdmin.Core.Middlewares
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private HttpContext _context;
        public LoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            _context = httpContext;
            var requestBody = string.Empty;
            if (_context.Request.Method != HttpMethod.Get.Method && !_context.Request.HasFormContentType)
            {
                _context.Request.EnableBuffering();
                requestBody = await new StreamReader(_context.Request.Body).ReadToEndAsync();
                _context.Request.Body.Position = 0;
            }
            string responseBody = string.Empty;
            Stream originalBody = _context.Response.Body;
            var elapsedResponseTimeInMilliseconds = 0L;

            try
            {
                using var memoryStream = new MemoryStream();
                _context.Response.Body = memoryStream;

                var timer = Stopwatch.StartNew();

                await _next(_context);

                timer.Stop();
                elapsedResponseTimeInMilliseconds = timer.ElapsedMilliseconds;

                memoryStream.Position = 0;
                responseBody = new StreamReader(memoryStream).ReadToEnd();
                memoryStream.Position = 0;
                await memoryStream.CopyToAsync(originalBody);
            }
            finally
            {
                _context.Response.Body = originalBody;
                CreateLog(requestBody, responseBody, elapsedResponseTimeInMilliseconds);
            }


        }
        private void CreateLog(string requestBody, string responseBody, long elapsedResponseTimeInMilliseconds)
        {
            LogDetail logDetail = new()
            {
                Action = CurrentAction,
                Controller = CurrentController,
                IpAddress = CurrentIpAddress,
                HttpMethod = CurrentHttpMethod,
                ResponseHttpStatusCode = CurrentResponseStatusCode,
                QueryString = CurrentQueryString,
                BrowserName = CurrentUserAgent,
                HttpHeaders = CurrentRequestHeaders,
                RouteValuesJson = CurrentRouteValues,
                UserId = CurrentUserId,
                RequestBody = requestBody,
                ResponseBody = responseBody,
                ResponseTimeInMilliseconds = elapsedResponseTimeInMilliseconds,
            };
            var loggerServiceBase = _context.RequestServices.GetRequiredService<LoggerServiceBase>();
            loggerServiceBase.Info(JsonConvert.SerializeObject(logDetail));
        }

        private string GetRequestHeaders()
        {
            var request = _context.Request;
            var headerList = new Dictionary<string, string>(request.Headers.Count);
            foreach (var header in request.Headers)
            {
                headerList.Add(header.Key, header.Value);
            }

            return JsonConvert.SerializeObject(headerList);
        }

        private string GetRouteValues()
        {
            var routeData = _context.GetRouteData();
            var routeValues = routeData?.Values;

            if (routeValues == null || routeValues.Count == 0)
            {
                return string.Empty;
            }

            return JsonConvert.SerializeObject(routeValues);
        }

        
        private string CurrentUserAgent => _context.Request.Headers["User-Agent"].ToString();

        private string CurrentIpAddress => _context.Connection.RemoteIpAddress.ToString();

        private string CurrentHttpMethod => _context.Request.Method;

        private string CurrentQueryString => _context.Request.QueryString.ToString();

        private string CurrentController => _context.GetRouteValue("Controller")?.ToString() ?? string.Empty;

        private string CurrentAction => _context.GetRouteValue("Action")?.ToString() ?? string.Empty;

        private int CurrentResponseStatusCode => _context.Response.StatusCode;

        private int CurrentUserId => _context.User.GetUserId();

        private string CurrentRouteValues => GetRouteValues();

        private string CurrentRequestHeaders => GetRequestHeaders();

    }
}
