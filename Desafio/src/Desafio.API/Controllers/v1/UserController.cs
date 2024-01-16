using Desafio.Application;
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
    /// Retorna todos os usuários
    /// </summary>
    /// <remarks>Retorna todos os usuários cadastrados</remarks>
    /// <returns></returns>
    [Authorize(Roles = "ADMINISTRATOR, MANAGER, SELLER")]
    [HttpGet("get-all-users")]
    public async Task<ActionResult<UserResponse>> GetAllUsers()
    {
        IEnumerable<UserResponse> result = await _userService.GetAllAsync(false);

        if (result.Count() == 0) return CustomResponse(result, "No users were found.");

        return CustomResponse(result);
    }

    /// <summary>
    /// Retorna todos os Administradores
    /// </summary>
    /// <remarks>Retorna todos os usuários do tipo Administrador</remarks>
    /// <returns></returns>
    [Authorize(Roles = "ADMINISTRATOR, MANAGER, SELLER")]
    [HttpGet("get-all-administrator-users")]
    public async Task<ActionResult<UserResponse>> GetAllAdministratorUsers()
    {
        IEnumerable<UserResponse> result = await _userService.GetAllUsersByRoleAsync(EUserLevel.Administrator.ToString().ToUpper());

        if (result.Count() == 0) return CustomResponse(result, "No administrator users were found.");

        return CustomResponse(result);
    }

    /// <summary>
    /// Retorna todos os Gerentes
    /// </summary>
    /// <remarks>Retorna todos os usuários do tipo Gerente</remarks>
    /// <returns></returns>
    [Authorize(Roles = "ADMINISTRATOR, MANAGER, SELLER")]
    [HttpGet("get-all-manager-users")]
    public async Task<ActionResult<UserResponse>> GetAllManagerUsers()
    {
        IEnumerable<UserResponse> result = await _userService.GetAllUsersByRoleAsync(EUserLevel.Manager.ToString().ToUpper());

        if (result.Count() == 0) return CustomResponse(result, "No manager users were found.");

        return CustomResponse(result);
    }

    /// <summary>
    /// Retorna todos os Vendedores
    /// </summary>
    /// <remarks>Retorna todos os usuários do tipo Vendedor</remarks>
    /// <returns></returns>
    [Authorize(Roles = "ADMINISTRATOR, MANAGER, SELLER")]
    [HttpGet("get-all-seller-users")]
    public async Task<ActionResult<UserResponse>> GetAllSellerUsers()
    {
        IEnumerable<UserResponse> result = await _userService.GetAllUsersByRoleAsync(EUserLevel.Seller.ToString().ToUpper());

        if (result.Count() == 0) return CustomResponse(result, "No seller users were found.");

        return CustomResponse(result);
    }

    /// <summary>
    /// Retornar usuário por Short Id
    /// </summary>
    /// <remarks>Retorna um usuário específica, pesquisando por seu Short Id</remarks>
    /// <param name="shortId"></param>
    /// <returns></returns>
    [Authorize(Roles = "ADMINISTRATOR, MANAGER, SELLER")]
    [HttpGet("get-by-short-id")]
    public async Task<ActionResult<UserResponse>> GetUnitByShortIdAsync(string shortId)
    {
        UserResponse result = await _userService.GetByShortIdAsync(shortId);

        return CustomResponse(result);

    }
    #endregion

    #region Put
    /// <summary>
    /// Atualizar usuário
    /// </summary>
    /// <remarks>Atualiza informações de um usuário</remarks>
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
    /// Atualizar Login
    /// </summary>
    /// <remarks>Atualiza senha do usuário</remarks>
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
    /// Excluir usuário
    /// </summary>
    /// <remarks>Exclui um cadastro de usuário</remarks>
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
