using Desafio.Application;
using Desafio.Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading;

namespace Desafio.API;
[ApiVersion("1.0")]
public class AuthorizationController : DesafioControllerBase
{
    public AuthorizationController(IError error) : base(error)
    {

    }

    #region Post
    /// <summary>
    /// Create User
    /// </summary>
    /// <remarks>Cria um novo usuário</remarks>
    /// <param name="mediator"></param>
    /// <param name="createUserRequest"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [Authorize(Roles = "ADMINISTRATOR, MANAGER, SELLER")]
    [HttpPost("register-user")]
    public async Task<ActionResult<CreateUserResponse>> RegisterUserAsync(ISender mediator, 
        CreateUserRequest createUserRequest,
        CancellationToken cancellationToken = default)
    {
        return await mediator.Send(createUserRequest, cancellationToken);
    }

    /// <summary>
    /// Login User 
    /// </summary>
    /// <remarks>Login de Usuário</remarks>
    /// <param name="mediator"></param>
    /// <param name="loginUserRequest"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("login")]
    public async Task<ActionResult<LoginUserResponse>> LoginUserAsync(ISender mediator,
        LoginUserRequest loginUserRequest,
        CancellationToken cancellationToken = default)
    {
        return await mediator.Send(loginUserRequest, cancellationToken);
    }
    #endregion

    #region Get 
    /// <summary>
    /// Select User Roles
    /// </summary>
    /// <remarks>Seleciona todos os usuários cadastrados, informando suas permissões</remarks>
    /// <returns></returns>
    [Authorize(Roles = "ADMINISTRATOR, MANAGER, SELLER")]
    [HttpGet("get-all-users-roles")]
    public async Task<ActionResult<GetUserResponse>> GetAllUsersRoles(ISender mediator,
        bool enable, 
        EUserLevel? userLevel,
        CancellationToken cancellationToken)
    {
        return await mediator.Send(new GetUserRequest(enable, userLevel), cancellationToken);
    }
    #endregion
}
