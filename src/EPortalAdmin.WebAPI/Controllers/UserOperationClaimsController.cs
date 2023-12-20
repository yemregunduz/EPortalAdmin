using EPortalAdmin.Application.Features.UserOperationClaims.Commands;
using EPortalAdmin.Application.Features.UserOperationClaims.Queries;
using EPortalAdmin.Core.Attributes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;

namespace EPortalAdmin.WebAPI.Controllers
{
    [Route("api/user-operation-claim-management")]
    [ApiController]
    public class UserOperationClaimsController : BaseController
    {
        [HttpGet("user-operation-claims/query")]
        [ExplorableEndpoint(Description = "OData Query Kullanarak Kullanıcı ve Yetki Listeleme")]
        [EnableQuery]
        public async Task<IActionResult> GetQueryableUserOperationClaims()
        {
            GetQueryableUserOperationClaimsQuery getQueryableUserOperationClaimsQuery = new();
            var result = await Mediator.Send(getQueryableUserOperationClaimsQuery);
            return Ok(result);
        }

        [HttpGet("user-operation-claims/{id}")]
        [ExplorableEndpoint(Description = "Id Bazlı Kullanıcı Yetki Sorgulama")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            GetUserOperationClaimByIdQuery getUserOperationClaimByIdQuery = new() { Id = id };
            var result = await Mediator.Send(getUserOperationClaimByIdQuery);
            return Ok(result);
        }

        [ExplorableEndpoint(Description = "User Id Bazlı Yetki Listeleme")]
        [HttpGet("user-operation-claims/all/user/{userId}")]
        public async Task<IActionResult> GetAllOperationClaimsByUserId([FromRoute] int userId)
        {
            GetAllOperationsClaimsByUserIdQuery getAllOperationsClaimsByUserIdQuery = new() { UserId = userId };
            var result = await Mediator.Send(getAllOperationsClaimsByUserIdQuery);
            return Ok(result);
        }

        [HttpPost("user-operation-claims")]
        [ExplorableEndpoint(Description = "Kullanıcı Yetki Ekleme")]
        public async Task<IActionResult> Create([FromBody] CreateUserOperationClaimCommand createUserOperationClaimCommand)
        {
            var result = await Mediator.Send(createUserOperationClaimCommand);
            return Created("", result);
        }

        [HttpPut("user-operation-claims")]
        [ExplorableEndpoint(Description = "Kullanıcı Yetki Güncelleme")]
        public async Task<IActionResult> Update([FromBody] UpdateUserOperationClaimCommand updateUserOperationClaimCommand)
        {
            var result = await Mediator.Send(updateUserOperationClaimCommand);
            return Ok(result);
        }

        [HttpDelete("user-operation-claims/{id}")]
        [ExplorableEndpoint(Description = "Kullanıcı Yetki Silme")]
        public async Task<IActionResult> DeleteById([FromRoute] int id)
        {
            DeleteUserOperationClaimByIdCommand deleteUserOperationClaimByIdCommand = new() { Id = id };
            var result = await Mediator.Send(deleteUserOperationClaimByIdCommand);
            return Ok(result);
        }

        [HttpDelete("user-operation-claims/{id}/soft-delete")]
        [ExplorableEndpoint(Description = "Kullanıcı Yetki Pasife Çekme")]
        public async Task<IActionResult> SoftDeleteById([FromRoute] int id)
        {
            SoftDeleteUserOperationClaimByIdCommand softDeleteUserOperationClaimByIdCommand = new() { Id = id };
            var result = await Mediator.Send(softDeleteUserOperationClaimByIdCommand);
            return Ok(result);
        }

        [HttpDelete("user-operation-claims/all/user/{userId}")]
        [ExplorableEndpoint(Description = "Kullanıcı Yetkilerini Silme")]
        public async Task<IActionResult> DeleteUserClaimsByUserId([FromRoute] int userId)
        {
            BulkDeleteUserOperationClaimsByUserIdCommand bulkDeleteUserOperationClaimsByUserId = new() { UserId = userId };
            var result = await Mediator.Send(bulkDeleteUserOperationClaimsByUserId);
            return Ok(result);
        }

        [HttpDelete("user-operation-claims/all/user/{userId}/soft-delete")]
        [ExplorableEndpoint(Description = "Kullanıcı Yetkilerini Pasife Çekme")]
        public async Task<IActionResult> SoftDeleteUserClaimsByUserId([FromRoute] int userId)
        {
            SoftBulkDeleteUserOperationClaimsByUserId
                softBulkDeleteUserOperationClaimsByUserId = new() { UserId = userId };
            var result = await Mediator.Send(softBulkDeleteUserOperationClaimsByUserId);
            return Ok(result);
        }

        [HttpDelete("user-operation-claims/all/operation-claim/{operationClaimId}")]
        [ExplorableEndpoint(Description = "Yetki Id Bazlı Kullanıcı Yetkilerini Silme")]
        public async Task<IActionResult> DeleteUsersClaimByOperationId([FromRoute] int operationClaimId)
        {
            BulkDeleteUserOperationClaimsByOperationClaimIdCommand bulkDeleteUserOperationClaimsByOperationClaimId = new() { OperationClaimId = operationClaimId };
            var result = await Mediator.Send(bulkDeleteUserOperationClaimsByOperationClaimId);
            return Ok(result);
        }

        [HttpDelete("user-operation-claims/all/operation-claim/{operationClaimId}/soft-delete")]
        [ExplorableEndpoint(Description = "Yetki Id Bazlı Kullanıcı Yetkilerini Pasife Çekme")]
        public async Task<IActionResult> SoftDeleteUsersClaimByOperationId([FromRoute] int operationClaimId)
        {
            SoftBulkDeleteUserOperationClaimsByOperationClaimId
                softBulkDeleteUserOperationClaimsByOperationClaimId = new() { OperationClaimId = operationClaimId };
            var result = await Mediator.Send(softBulkDeleteUserOperationClaimsByOperationClaimId);
            return Ok(result);
        }
    }
}
