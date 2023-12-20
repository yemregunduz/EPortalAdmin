using EPortalAdmin.Application.Features.Users.Queries;
using EPortalAdmin.Application.ViewModels.User;
using EPortalAdmin.Application.Wrappers.Results;
using EPortalAdmin.Core.Attributes;
using EPortalAdmin.Core.Domain.Models;
using EPortalAdmin.Core.Exceptions;
using EPortalAdmin.Core.Persistence.Dynamic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;

namespace EPortalAdmin.WebAPI.Controllers
{
    [Authorize]
    [Route("api/user-management")]
    [ApiController]
    public class UsersController : BaseController
    {
        /// <summary>
        /// Retrieves a list of users with optional paging.
        /// </summary>
        /// <param name="pagingRequest">Paging parameters for the user list.</param>
        /// <returns>Returns a list of users.</returns>
        /// <response code="200">Returns the user list.</response>
        /// <response code="400">Bad Request if there's an issue with the request.</response>
        [HttpGet("users")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DataResult<UserListDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BusinessProblemDetails))]
        [ExplorableEndpoint(Description = "Kullanıcı Listeleme")]
        public async Task<IActionResult> GetUserList([FromQuery] PagingRequest pagingRequest)
        {
            GetUserListQuery getUserListQuery = new() { PagingRequest = pagingRequest };
            var result = await Mediator.Send(getUserListQuery);
            return Ok(result);
        }

        /// <summary>
        /// Retrieves a list of users with OData Query
        /// </summary>
        /// <response code="200">Returns the user list.</response>
        /// <response code="400">Bad Request if there's an issue with the request.</response>
        [HttpGet("users/query")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IQueryable<UserDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BusinessProblemDetails))]
        [ExplorableEndpoint(Description = "OData Query Kullanarak Kullanıcı Listeleme")]
        [EnableQuery]
        public async Task<IActionResult> GetQueryableUsers()
        {
            GetQueryableUsersQuery getUserOpenDataQuery = new();
            var result = await Mediator.Send(getUserOpenDataQuery);
            return Ok(result);
        }

        /// <summary>
        /// Retrieves a list of users based on dynamic search criteria with optional paging.
        /// </summary>
        /// <param name="pagingRequest">Paging parameters for the user list.</param>
        /// <param name="dynamic">Dynamic search criteria.</param>
        /// <returns>Returns a list of users based on the dynamic search.</returns>
        /// <response code="200">Returns the user list.</response>
        /// <response code="400">Bad Request if there's an issue with the request.</response>
        [HttpPost("users/search")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DataResult<UserListDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BusinessProblemDetails))]
        [ExplorableEndpoint(Description = "Dinamik Sorgu ile Kullanıcı Listeleme")]
        public async Task<IActionResult> GetUserListByDynamic([FromQuery] PagingRequest pagingRequest, [FromBody] Dynamic dynamic)
        {
            GetUserListByDynamicQuery getUserListByDynamicQuery = new() { PagingRequest = pagingRequest, Dynamic = dynamic };
            var result = await Mediator.Send(getUserListByDynamicQuery);
            return Ok(result);
        }

        /// <summary>
        /// Retrieves a user by their ID.
        /// </summary>
        /// <param name="getUserByIdQuery">Query containing the user ID.</param>
        /// <returns>Returns the user by ID.</returns>
        /// <response code="200">Returns the user by ID.</response>
        /// <response code="400">Bad Request if there's an issue with the request.</response>
        [HttpGet("users/{Id}")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DataResult<UserDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BusinessProblemDetails))]
        [ExplorableEndpoint(Description = "Id Bazlı Kullanıcı Sorgulama")]
        public async Task<IActionResult> GetUserById([FromRoute] GetUserByIdQuery getUserByIdQuery)
        {
            var result = await Mediator.Send(getUserByIdQuery);
            return Ok(result);
        }

        /// <summary>
        /// Retrieves a user by their email address.
        /// </summary>
        /// <param name="getUserByEmailAddressQuery">Query containing the email address.</param>
        /// <returns>Returns the user by email address.</returns>
        /// <response code="200">Returns the user by email address.</response>
        /// <response code="400">Bad Request if there's an issue with the request.</response>
        [HttpGet("users/email-address/{emailAddress}")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DataResult<UserDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BusinessProblemDetails))]
        [ExplorableEndpoint(Description = "Email Bazlı Kullanıcı Sorgulama")]
        public async Task<IActionResult> GetUserByEmailAddress([FromRoute] GetUserByEmailAddressQuery getUserByEmailAddressQuery)
        {
            var result = await Mediator.Send(getUserByEmailAddressQuery);
            return Ok(result);
        }

    }
}
