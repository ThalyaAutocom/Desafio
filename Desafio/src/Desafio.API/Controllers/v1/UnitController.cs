using Desafio.Application;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Desafio.API;
[ApiVersion("1.0")]
public class UnitController : DesafioControllerBase
{
    private readonly IUnitService _unitService;

    public UnitController(IUnitService unitService, IError error) : base(error)
    {
        _unitService = unitService;
    }

    #region Get
    /// <summary>
    /// Retornar unidade por Símbolo de unidade de Medida
    /// </summary>
    /// <param name="acronym"></param>
    /// <returns></returns>
    [Authorize(Roles = "ADMINISTRATOR, MANAGER, SELLER")]
    [HttpGet("get-by-acronym")]
    public async Task<ActionResult<UnitResponse>> GetUnitAsync(string acronym)
    {
        UnitResponse result = await _unitService.GetByAcronymAsync(acronym);

        return CustomResponse(result);

    }

    /// <summary>
    /// Retornar unidade por Short Id
    /// </summary>
    /// <param name="shortId"></param>
    /// <returns></returns>
    [Authorize(Roles = "ADMINISTRATOR, MANAGER, SELLER")]
    [HttpGet("get-by-short-id")]
    public async Task<ActionResult<UnitResponse>> GetUnitByShortIdAsync(string shortId)
    {
        UnitResponse result = await _unitService.GetByShortIdAsync(shortId);

        return CustomResponse(result);

    }

    /// <summary>
    /// Retornar todas as unidades
    /// </summary>
    /// <returns></returns>
    [Authorize(Roles = "ADMINISTRATOR, MANAGER, SELLER")]
    [HttpGet("get-all")]
    public async Task<ActionResult<IEnumerable<UnitResponse>>> GetAllUnitSAsync()
    {
        IEnumerable<UnitResponse> result = await _unitService.GetAllAsync();
        if (!result.Any()) return CustomResponseList(result, "No units were found.");

        return CustomResponseList(result);
    }
    #endregion

    #region Post
    /// <summary>
    /// Cadastrar Unidade
    /// </summary>
    /// <param name="unitRequest"></param>
    /// <returns></returns>
    [Authorize(Roles = "ADMINISTRATOR, MANAGER")]
    [HttpPost("insert-unit")]
    public async Task<ActionResult<UnitResponse>> InsertUnitAsync(UnitRequest unitRequest)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        UnitResponse result = await _unitService.InsertAsync(unitRequest);

        return CustomResponse(result);
    }
    #endregion

    #region Put
    /// <summary>
    /// Atualizar unidade
    /// </summary>
    /// <param name="unitRequest"></param>
    /// <returns></returns>
    [Authorize(Roles = "ADMINISTRATOR, MANAGER")]
    [HttpPut("update-unit")]
    public async Task<ActionResult<UnitResponse>> UpdateUnitAsync(UnitRequest unitRequest)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        UnitResponse result = await _unitService.UpdateAsync(unitRequest);

        return CustomResponse(result);

    }
    #endregion

    #region Delete
    /// <summary>
    /// Ecluir Unidade
    /// </summary>
    /// <param name="acronym"></param>
    /// <returns></returns>
    [Authorize(Roles = "ADMINISTRATOR, MANAGER")]
    [HttpDelete("delete-unit")]
    public async Task<ActionResult<UnitResponse>> DeleteUnitAsync(string acronym)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        UnitResponse result = await _unitService.RemoveAsync(acronym.ToUpper());

        return CustomResponse(result);
    }
    #endregion
}
