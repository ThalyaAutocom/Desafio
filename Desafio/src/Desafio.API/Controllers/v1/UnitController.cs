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
    /// Select Unit by Acronym
    /// </summary>
    /// <remarks>Searchs an especifc unit on database, using its acronym</remarks>
    /// <param name="acronym"></param>
    /// <returns></returns>
    [HttpGet("get-by-acronym")]
    public async Task<ActionResult<UnitResponse>> GetUnitAsync(string acronym)
    {
        UnitResponse result = await _unitService.GetByAcronymAsync(acronym);

        return CustomResponse(result);

    }

    /// <summary>
    /// Select Unit by Short Id
    /// </summary>
    /// <remarks>Searchs an especific unit on database, using its Short Id</remarks>
    /// <param name="shortId"></param>
    /// <returns></returns>
    [HttpGet("get-by-short-id")]
    public async Task<ActionResult<UnitResponse>> GetUnitByShortIdAsync(string shortId)
    {
        UnitResponse result = await _unitService.GetByShortIdAsync(shortId);

        return CustomResponse(result);

    }

    /// <summary>
    /// Select all Units
    /// </summary>
    /// <remarks>Searchs all units registered in database</remarks>
    /// <returns></returns>
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
    /// Insert Unit
    /// </summary>
    /// <remarks>Inserts one unit on database</remarks>
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
    /// Update Unit
    /// </summary>
    /// <remarks>Updates an unit information</remarks>
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
    /// Delete Unit
    /// </summary>
    /// <remarks>Removes an especific unit from the database</remarks>
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
