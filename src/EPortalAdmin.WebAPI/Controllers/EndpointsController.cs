using EPortalAdmin.Application.Features.Endpoints.Queries;
using EPortalAdmin.Application.ViewModels.Endpoint;
using EPortalAdmin.Application.Wrappers.Results;
using EPortalAdmin.Core.Attributes;
using EPortalAdmin.Core.Domain.Models;
using EPortalAdmin.Core.Exceptions;
using EPortalAdmin.Core.Persistence.Dynamic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;

namespace EPortalAdmin.WebAPI.Controllers
{
    [Route("api/endpoint-management")]
    [ApiController]
    public class EndpointsController : BaseController
    {

        [HttpGet("endpoints")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DataResult<EndpointListDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BusinessProblemDetails))]
        [ExplorableEndpoint(Description = "Endpoint Listeleme")]
        public async Task<IActionResult> GetEndpointList([FromQuery] PagingRequest pagingRequest)
        {
            GetEndpointListQuery getEndpointListQuery = new() { PagingRequest = pagingRequest };
            var result = await Mediator.Send(getEndpointListQuery);
            return Ok(result);
        }

        [HttpGet("endpoints/search")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DataResult<EndpointListDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BusinessProblemDetails))]
        [ExplorableEndpoint(Description = "Dinamik Sorgu ile Endpoint Listeleme")]
        public async Task<IActionResult> GetEndpointListByDynamic([FromQuery] PagingRequest pagingRequest, [FromBody] Dynamic dynamic)
        {
            GetEndpointListByDynamicQuery getEndpointListByDynamicQuery =
                new() { PagingRequest = pagingRequest, Dynamic = dynamic };
            var result = await Mediator.Send(getEndpointListByDynamicQuery);
            return Ok(result);
        }

        [HttpGet("endpoints/query")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IQueryable<EndpointDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BusinessProblemDetails))]
        [ExplorableEndpoint(Description = "OData Query Kullanarak Endpoint Listeleme")]
        [EnableQuery]
        public async Task<IActionResult> GetQueryableEndpoints()
        {
            GetQueryableEndpointsQuery getQueryableEndpointsQuery = new();
            var result = await Mediator.Send(getQueryableEndpointsQuery);
            return Ok(result);
        }

        [HttpGet("endpoints/{id}")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DataResult<EndpointDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BusinessProblemDetails))]
        [ExplorableEndpoint(Description = "Id Bazlı Endpoint Bilgisi Sorgulama")]
        public async Task<IActionResult> GetEndpointById([FromRoute] int id)
        {
            GetEndpointByIdQuery getEndpointByIdQuery = new() { Id = id };
            var result = await Mediator.Send(getEndpointByIdQuery);
            return Ok(result);
        }
    }
}
