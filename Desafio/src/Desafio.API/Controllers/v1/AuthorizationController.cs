﻿using Desafio.Application;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Desafio.API;
[ApiVersion("1.0")]
public class AuthorizationController : DesafioControllerBase
{
    private IUserService _userService;

    public AuthorizationController(IUserService identityService, IError error) : base(error)
    {
        _userService = identityService;
    }

    #region Post
    /// <summary>
    /// Create User
    /// </summary>
    /// <remarks>Cria um novo usuário</remarks>
    /// <param name="registerUserRequest"></param>
    /// <returns></returns>
    [Authorize(Roles = "ADMINISTRATOR, MANAGER, SELLER")]
    [HttpPost("register-user")]
    public async Task<ActionResult<RegisterUserResponse>> RegisterUserAsync(RegisterUserRequest registerUserRequest)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        RegisterUserResponse result = await _userService.RegisterUserAsync(registerUserRequest, User);

        return CustomResponse(result);
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
