using EPortalAdmin.Core.Utilities.Extensions.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Newtonsoft.Json;

namespace EPortalAdmin.Core.Utilities.Extensions
{
    public static class HttpContextExtensions
    {
        public static string GetCurrentRequestHeaders(this HttpContext context)
        {
            var headerList = new Dictionary<string, string>(context.Request.Headers.Count);

            foreach (var header in context.Request.Headers)
            {
                headerList.Add(header.Key, header.Value);
            }

            return JsonConvert.SerializeObject(headerList);
        }

        public static string GetCurrentRouteValues(this HttpContext context)
        {
            var routeData = context.GetRouteData();
            var routeValues = routeData?.Values;

            if (routeValues == null || routeValues.Count == 0)
            {
                return string.Empty;
            }

            return JsonConvert.SerializeObject(routeValues);
        }

        public static string GetCurrentIpAddress(this HttpContext context) => context.Connection.RemoteIpAddress.ToString();

        public static string GetCurrentHttpMethod(this HttpContext context) => context.Request.Method;

        public static string GetCurrentQueryString(this HttpContext context) => context.Request.QueryString.ToString();

        public static string GetCurrentController(this HttpContext context) => context.GetRouteValue("Controller")?.ToString() ?? string.Empty;

        public static string GetCurrentAction(this HttpContext context) => context.GetRouteValue("Action")?.ToString() ?? string.Empty;

        public static string GetCurrentUserAgent(this HttpContext context) => context.Request.Headers["User-Agent"].ToString();

        public static int GetCurrentResponseStatusCode(this HttpContext context) => context.Response.StatusCode;

        public static int GetCurrentUserId(this HttpContext context) => context.User.GetUserId();


    }
}
