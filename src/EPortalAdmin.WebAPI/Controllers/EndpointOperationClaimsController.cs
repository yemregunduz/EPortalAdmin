using EPortalAdmin.Application.Features.EndpointOperationClaims.Commands;
using EPortalAdmin.Application.ViewModels.EndpointOperationClaim;
using EPortalAdmin.Core.Attributes;
using EPortalAdmin.Core.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;

namespace EPortalAdmin.WebAPI.Controllers
{
    [Route("api/endpoint-operation-claim-management")]
    [ApiController]
    public class EndpointOperationClaimsController : BaseController
    {
        [HttpGet("endpoint-operation-claims/query")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IQueryable<EndpointOperationClaimDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BusinessProblemDetails))]
        [ExplorableEndpoint(Description = "OData Query Kullanarak Endpoint ve Yetkilerini Listeleme")]
        [EnableQuery]
        public async Task<IActionResult> GetQueryableEndpointOperationClaims()
        {
            GetQueryableEndpointOperationClaimsQuery getQueryableEndpointOperationClaimsQuery = new();
            var result = await Mediator.Send(getQueryableEndpointOperationClaimsQuery);
            return Ok(result);
        }

        [HttpPost("endpoint-operation-claims")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BusinessProblemDetails))]
        [ExplorableEndpoint(Description = "Endpoint Yetki Ekleme")]
        public async Task<ActionResult> Create(CreateEndpointOperationClaimCommand command)
        {
            var result = await Mediator.Send(command);
            return Created("",result);
        }

        [HttpDelete("endpoint-operation-claims/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BusinessProblemDetails))]
        [ExplorableEndpoint(Description = "Endpoint Yetki Silme")]
        public async Task<ActionResult> Delete(int id)
        {
            DeleteEndpointOperationClaimByIdCommand deleteEndpointOperationClaimByIdCommand = new() { Id = id };
            var result = await Mediator.Send(deleteEndpointOperationClaimByIdCommand);
            return Ok(result);
        }

        [HttpPut("endpoint-operation-claims")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ExplorableEndpoint(Description = "Endpoint Yetki Güncelleme")]
        public async Task<ActionResult> Update(UpdateEndpointOperationClaimCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }

    }
}
