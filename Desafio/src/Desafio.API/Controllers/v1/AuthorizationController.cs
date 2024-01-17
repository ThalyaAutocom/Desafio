using Desafio.Application;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Desafio.API;
[ApiVersion("1.0")]
public class AuthorizationController : DesafioControllerBase
{
    private IUserService _userService;
    private IMediator _mediator;

    public AuthorizationController(IUserService identityService, IError error, IMediator mediator) : base(error)
    {
        _userService = identityService;
        _mediator = mediator;
    }

    #region Post
    /// <summary>
    /// Create User
    /// </summary>
    /// <remarks>Cria um novo usuário</remarks>
    /// <param name="createUserRequest"></param>
    /// <returns></returns>
    [Authorize(Roles = "ADMINISTRATOR, MANAGER, SELLER")]
    [HttpPost("register-user")]
    public async Task<CreateUserResponse> RegisterUserAsync(CreateUserRequest createUserRequest)
    {
        //if (!ModelState.IsValid) return CustomResponse(ModelState);

        //CreateUserResponse result = await _userService.InsertUserAsync(createUserRequest, User);

        return await _mediator.Send(createUserRequest);
    }

    /// <summary>
    /// Login User 
    /// </summary>
    /// <remarks>Login de Usuário</remarks>
    /// <param name="loginUserRequest"></param>
    /// <returns></returns>
    [HttpPost("login")]
    public async Task<ActionResult<LoginUserResponse>> LoginUserAsync(LoginUserRequest loginUserRequest)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        LoginUserResponse result = await _userService.LoginAsync(loginUserRequest);

        return CustomResponse(result);
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
    public async Task<ActionResult<UserResponse>> GetAllUsersRoles()
    {
        var result = await _userService.GetAllAsync(true);

        if (result.Count() == 0) return CustomResponse(result, "No users were found.");

        return CustomResponse(result);
    }
    #endregion
}
