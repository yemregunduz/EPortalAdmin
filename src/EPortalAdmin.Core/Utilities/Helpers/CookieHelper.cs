using Microsoft.AspNetCore.Http;
namespace EPortalAdmin.Core.Utilities.Helpers
{
    public static class CookieHelper
    {
        /// <summary>
        /// Sets a cookie with the specified name and value to be added to the HTTP response, using the provided HttpContext.
        /// </summary>
        /// <param name="httpContext">The HttpContext object representing the current request and response.</param>
        /// <param name="name">The name of the cookie.</param>
        /// <param name="value">The value of the cookie.</param>
        /// <param name="expireMinutes">The expiration time of the cookie in minutes. Defaults to null.</param>
        public static void SetCookie(HttpContext httpContext, string name, string value, int? expireMinutes = null)
        {
            if (httpContext == null)
                throw new ArgumentNullException(nameof(httpContext), "HttpContext cannot be null.");

            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Cookie name cannot be null or empty.", nameof(name));

            var cookieOptions = new CookieOptions();

            if (expireMinutes.HasValue)
                cookieOptions.Expires = DateTime.Now.AddMinutes(expireMinutes.Value);

            httpContext.Response.Cookies.Append(name, value, cookieOptions);
        }

        /// <summary>
        /// Retrieves the value of the specified cookie from the provided HttpContext.
        /// </summary>
        /// <param name="httpContext">The HttpContext object representing the current request and response.</param>
        /// <param name="name">The name of the cookie.</param>
        /// <returns>The value of the cookie as a string.</returns>
        public static string GetCookieValue(HttpContext httpContext, string name)
        {
            if (httpContext == null)
                throw new ArgumentNullException(nameof(httpContext), "HttpContext cannot be null.");

            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Cookie name cannot be null or empty.", nameof(name));

            return httpContext.Request.Cookies[name];
        }

        /// <summary>
        /// Deletes the specified cookie using the provided HttpContext.
        /// </summary>
        /// <param name="httpContext">The HttpContext object representing the current request and response.</param>
        /// <param name="name">The name of the cookie to be deleted.</param>
        public static void DeleteCookie(HttpContext httpContext, string name)
        {
            if (httpContext == null)
                throw new ArgumentNullException(nameof(httpContext), "HttpContext cannot be null.");

            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Cookie name cannot be null or empty.", nameof(name));

            httpContext.Response.Cookies.Delete(name);
        }
    }

}
