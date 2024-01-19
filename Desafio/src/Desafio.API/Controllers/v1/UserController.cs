using Desafio.Application;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Desafio.API;
[ApiVersion("1.0")]
public class UserController : DesafioControllerBase
{
    public UserController(IError error) : base(error)
    {
        
    }

    #region Get
    /// <summary>
    /// Select User by Short Id
    /// </summary>
    /// <param name="mediator"></param>
    /// <param name="shortId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [Authorize(Roles = "ADMINISTRATOR, MANAGER, SELLER")]
    [HttpGet("get-by-short-id")]
    public async Task<ActionResult<UserResponse>> GetUnitByShortIdAsync(ISender mediator,
        string shortId,
        CancellationToken cancellationToken = default)
    {
        return await mediator.Send(new GetByShortIdUserRequest(shortId), cancellationToken);

    }

    /// <summary>
    /// Select User by Id
    /// </summary>
    /// <param name="mediator"></param>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [Authorize(Roles = "ADMINISTRATOR, MANAGER, SELLER")]
    [HttpGet("get-by-id")]
    public async Task<ActionResult<UserResponse>> GetUnitByIdAsync(ISender mediator,
        string id,
        CancellationToken cancellationToken = default)
    {
        return await mediator.Send(new GetByIdUserRequest(id), cancellationToken);
    }
    #endregion

    #region Put
    /// <summary>
    /// Update User
    /// </summary>
    /// <param name="mediator"></param>
    /// <param name="userRequest"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [Authorize(Roles = "ADMINISTRATOR, MANAGER")]
    [HttpPut("update-user-informations")]
    public async Task<bool> UpdateUserAsync(ISender mediator,
        UpdateUserRequest userRequest,
        CancellationToken cancellationToken = default)
    {
        return await mediator.Send(userRequest, cancellationToken);

    }

    /// <summary>
    /// Update Login
    /// </summary>
    /// <param name="mediator"></param>
    /// <param name="userRequest"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [Authorize(Roles = "ADMINISTRATOR")]
    [HttpPut("update-login")]
    public async Task<bool> UpdateLoginUserAsync(ISender mediator,
        UpdateLoginUserRequest userRequest,
        CancellationToken cancellationToken = default)
    {
        return await mediator.Send(userRequest, cancellationToken);

    }
    #endregion

    #region Delete
    /// <summary>
    /// Remove user
    /// </summary>
    /// <param name="mediatior"></param>
    /// <param name="userRequest"></param>
    /// <param name="cancellationToken"></param>
    /// <param name="email"></param>
    /// <returns></returns>
    [Authorize(Roles = "ADMINISTRATOR, MANAGER")]
    [HttpDelete("delete-user")]
    public async Task<bool> DeleteUserAsync(ISender mediatior,
        DeleteUserRequest userRequest,
        CancellationToken cancellationToken = default)
    {
        return await mediatior.Send(userRequest, cancellationToken);

    }
    #endregion
}
