using EPortalAdmin.Core.Logging;
using EPortalAdmin.Core.Logging.Serilog;
using EPortalAdmin.Core.Utilities.Helpers;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Diagnostics;

namespace EPortalAdmin.Core.Middlewares
{
    public class LoggingMiddleware(LoggerServiceBase loggerService) : IMiddleware
    {
        private HttpContext _context;

        public async Task InvokeAsync(HttpContext httpContext, RequestDelegate next)
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

                await next(_context);

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
                CreateServiceLog(requestBody, responseBody, elapsedResponseTimeInMilliseconds);
            }


        }
        private void CreateServiceLog(string requestBody, string responseBody, long elapsedResponseTimeInMilliseconds)
        {
            ServiceLog serviceLog = LoggingHelper.GetServiceLog(_context, requestBody, responseBody, elapsedResponseTimeInMilliseconds);
            SerilogHelpers.PushServiceLogProperty(serviceLog);

            loggerService.Info(JsonConvert.SerializeObject(serviceLog));
        }
    }
}
