using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EPortalAdmin.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected IMediator Mediator =>
            _mediator ??= HttpContext.RequestServices.GetService<IMediator>()
                ?? throw new InvalidOperationException("IMediator cannot be retrieved from request services.");
        private IMediator? _mediator;

        [NonAction]
        protected string GetIpAddress()
        {
            string ipAddress = Request.Headers.ContainsKey("X-Forwarded-For")
                ? Request.Headers["X-Forwarded-For"].ToString()
                : HttpContext.Connection.RemoteIpAddress?.MapToIPv4().ToString()
                    ?? throw new InvalidOperationException("IP address cannot be retrieved from request.");
            return ipAddress;
        }
    }
}
