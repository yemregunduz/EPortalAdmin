using EPortalAdmin.Application.Features.OperationClaims.Commands;
using EPortalAdmin.Application.Features.OperationClaims.Queries;
using EPortalAdmin.Core.Attributes;
using EPortalAdmin.Core.Domain.Models;
using EPortalAdmin.Core.Persistence.Dynamic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;

namespace EPortalAdmin.WebAPI.Controllers
{
    [Route("api/operation-claim-management")]
    [ApiController]
    public class OperationClaimsController : BaseController
    {
        [HttpGet("operation-claims")]
        [ExplorableEndpoint(Description = "Yetki Listeleme")]
        public async Task<IActionResult> GetList([FromQuery] PagingRequest pagingRequest)
        {
            GetOperationClaimListQuery getOperationClaimListQuery = new() { PagingRequest = pagingRequest };
            var result = await Mediator.Send(getOperationClaimListQuery);
            return Ok(result);
        }

        [HttpGet("operation-claims/{id}")]
        [ExplorableEndpoint(Description = "Id Bazlı Yetki Sorgulama")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            GetOperationClaimByIdQuery getOperationClaimByIdQuery = new() { Id = id };
            var result = await Mediator.Send(getOperationClaimByIdQuery);
            return Ok(result);
        }

        [HttpGet("operation-claims/query")]
        [ExplorableEndpoint(Description = "OData Query Kullanarak Yetki Listeleme")]
        [EnableQuery]
        public async Task<IActionResult> GetQueryable()
        {
            GetQueryableOperationClaimsQuery getQueryableOperationClaimsQuery = new();
            var result = await Mediator.Send(getQueryableOperationClaimsQuery);
            return Ok(result);
        }

        [HttpPost("operation-claims/search")]
        [ExplorableEndpoint(Description = "Dinamik Sorgu ile Yetki Listeleme")]
        public async Task<IActionResult> GetListByDynamic([FromQuery] PagingRequest pagingRequest, Dynamic dynamic)
        {
            GetOperationClaimListByDynamicQuery getOperationClaimListByDynamicQuery = new() { PagingRequest = pagingRequest, Dynamic = dynamic };
            var result = await Mediator.Send(getOperationClaimListByDynamicQuery);
            return Ok(result);
        }

        [HttpPost("operation-claims")]
        [ExplorableEndpoint(Description = "Yetki Ekleme")]
        public async Task<IActionResult> Create([FromBody] CreateOperationClaimCommand createOperationClaimCommand)
        {
            var result = await Mediator.Send(createOperationClaimCommand);
            return Created("",result);
        }

        [HttpPut("operation-claims")]
        [ExplorableEndpoint(Description = "Yetki Güncelleme")]
        public async Task<IActionResult> Update([FromBody] UpdateOperationClaimCommand updateOperationClaimCommand)
        {
            var result = await Mediator.Send(updateOperationClaimCommand);
            return Ok(result);
        }

        [HttpDelete("operation-claims/{id}")]
        [ExplorableEndpoint(Description = "Yetki Silme")]
        public async Task<IActionResult> DeleteById([FromRoute] int id)
        {
            DeleteOperationClaimByIdCommand deleteOperationClaimByIdCommand = new() { Id = id };
            var result = await Mediator.Send(deleteOperationClaimByIdCommand);
            return Ok(result);
        }

        [HttpDelete("operation-claims/{id}/soft-delete")]
        [ExplorableEndpoint(Description = "Yetkiyi Pasife Çekme")]
        public async Task<IActionResult> SoftDeleteById([FromRoute] int id)
        {
            SoftDeleteOperationClaimByIdCommand softDeleteOperationClaimByIdCommand = new() { Id = id };
            var result = await Mediator.Send(softDeleteOperationClaimByIdCommand);
            return Ok(result);
        }

        [HttpDelete("operation-claims/name/{name}")]
        [ExplorableEndpoint(Description = "İsim Bazlı Yetki Silme")]
        public async Task<IActionResult> DeleteByName([FromRoute] string name)
        {
            DeleteOperationClaimByNameCommand deleteOperationClaimByNameCommand = new() { Name = name };
            var result = await Mediator.Send(deleteOperationClaimByNameCommand);
            return Ok(result);
        }

        [HttpDelete("operation-claims/name/{name}/soft-SoftDelete")]
        [ExplorableEndpoint(Description = "İsim Bazlı Yetkiyi Pasife Çekme")]
        public async Task<IActionResult> SoftDeleteByName([FromRoute] string name)
        {
            SoftDeleteOperationClaimByNameCommand softDeleteOperationClaimByNameCommand = new() { Name = name };
            var result = await Mediator.Send(softDeleteOperationClaimByNameCommand);
            return Ok(result);
        }
    }
}
