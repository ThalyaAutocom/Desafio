﻿using Desafio.Application;
using Desafio.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Desafio.API;
[ApiVersion("1.0")]
public class UserController : DesafioControllerBase
{
    private IUserService _userService;

    public UserController(IUserService identityService, IError error) : base(error)
    {
        _userService = identityService;
    }

    #region Get
    /// <summary>
    /// Select all Users
    /// </summary>
    /// <remarks>Searchs all users registered in database</remarks>
    /// <returns></returns>
    [HttpGet("get-all-users")]
    public async Task<ActionResult<UserResponse>> GetAllUsers()
    {
        IEnumerable<UserResponse> result = await _userService.GetAllAsync(false);

        if (result.Count() == 0) return CustomResponse(result, "No users were found.");

        return CustomResponse(result);
    }

    /// <summary>
    /// Select all Administrator Users
    /// </summary>
    /// <remarks>Searchs all users registered in database that has an Administrator Role</remarks>
    /// <returns></returns>
    [HttpGet("get-all-administrator-users")]
    public async Task<ActionResult<UserResponse>> GetAllAdministratorUsers()
    {
        IEnumerable<UserResponse> result = await _userService.GetAllUsersByRoleAsync(EUserLevel.Administrator.ToString().ToUpper());

        if (result.Count() == 0) return CustomResponse(result, "No administrator users were found.");

        return CustomResponse(result);
    }

    /// <summary>
    /// Select all Manager Users
    /// </summary>
    /// <remarks>Searchs all users registered in database that has a Manager Role</remarks>
    /// <returns></returns>
    [HttpGet("get-all-manager-users")]
    public async Task<ActionResult<UserResponse>> GetAllManagerUsers()
    {
        IEnumerable<UserResponse> result = await _userService.GetAllUsersByRoleAsync(EUserLevel.Manager.ToString().ToUpper());

        if (result.Count() == 0) return CustomResponse(result, "No manager users were found.");

        return CustomResponse(result);
    }

    /// <summary>
    /// Select all Seller Users
    /// </summary>
    /// <remarks>Searchs all users registered in database that has a Seller Role</remarks>
    /// <returns></returns>
    [HttpGet("get-all-seller-users")]
    public async Task<ActionResult<UserResponse>> GetAllSellerUsers()
    {
        IEnumerable<UserResponse> result = await _userService.GetAllUsersByRoleAsync(EUserLevel.Seller.ToString().ToUpper());

        if (result.Count() == 0) return CustomResponse(result, "No seller users were found.");

        return CustomResponse(result);
    }

    /// <summary>
    /// Select User by Short Id
    /// </summary>
    /// <remarks>Searchs an especific user on database, using their Short Id</remarks>
    /// <param name="shortId"></param>
    /// <returns></returns>
    [HttpGet("get-by-short-id")]
    public async Task<ActionResult<UserResponse>> GetUnitByShortIdAsync(string shortId)
    {
        UserResponse result = await _userService.GetByShortIdAsync(shortId);

        return CustomResponse(result);

    }
    #endregion

    #region Put
    /// <summary>
    /// Update User
    /// </summary>
    /// <remarks>Updates an user information</remarks>
    /// <param name="userRequest"></param>
    /// <returns></returns>
    [Authorize(Roles = "ADMINISTRATOR, MANAGER")]
    [HttpPut("update-user-informations")]
    public async Task<ActionResult<UserResponse>> UpdateUserAsync(UpdateUserRequest userRequest)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        UserResponse result = await _userService.UpdateAsync(userRequest);

        return CustomResponse(result);

    }

    /// <summary>
    /// Update Login
    /// </summary>
    /// <remarks>Updates the password of an especific user</remarks>
    /// <param name="userRequest"></param>
    /// <returns></returns>
    [Authorize(Roles = "ADMINISTRATOR")]
    [HttpPut("update-login")]
    public async Task<ActionResult<UserResponse>> UpdateLoginUserAsync(UpdateLoginUserRequest userRequest)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        UserResponse result = await _userService.UpdateAsync(userRequest);

        return CustomResponse(result);

    }
    #endregion

    #region Delete
    /// <summary>
    /// Delete User
    /// </summary>
    /// <remarks>Removes an especific user from the database</remarks>
    /// <param name="email"></param>
    /// <returns></returns>
    [Authorize(Roles = "ADMINISTRATOR, MANAGER")]
    [HttpDelete("delete-user")]
    public async Task<ActionResult<UserResponse>> DeleteUserAsync(string email)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        UserResponse result = await _userService.RemoveAsync(email);

        return CustomResponse(result);

    }
    #endregion
}
