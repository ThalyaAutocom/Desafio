using Desafio.Application;
using Desafio.Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using System.Threading;

namespace Desafio.API;
[ApiVersion("1.0")]
public class UnitController : DesafioControllerBase
{
    public UnitController(IError error) : base(error)
    {
        
    }

    #region Get
    /// <summary>
    /// Retornar unidade por Símbolo de unidade de Medida
    /// </summary>
    /// <param name="mediator"></param>
    /// <param name="cancellationToken"></param>
    /// <param name="acronym"></param>
    /// <returns></returns>
    [Authorize(Roles = "ADMINISTRATOR, MANAGER, SELLER")]
    [HttpGet("get-by-acronym")]
    public async Task<ActionResult<UnitResponse>> GetUnitAsync(ISender mediator,
        string acronym,
        CancellationToken cancellationToken = default)
    {
        return await mediator.Send(new GetByAcronymUnitRequest(acronym), cancellationToken);

    }

    /// <summary>
    /// Retornar unidade por Short Id
    /// </summary>
    /// <param name="mediator"></param>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <param name="shortId"></param>
    /// <returns></returns>
    [Authorize(Roles = "ADMINISTRATOR, MANAGER, SELLER")]
    [HttpGet("get-by-short-id")]
    public async Task<ActionResult<UnitResponse>> GetUnitByShortIdAsync(ISender mediator,
        string shortId,
        CancellationToken cancellationToken = default)
    {
        return await mediator.Send(new GetByShortIdUnitRequest(shortId), cancellationToken);

    }

    /// <summary>
    /// Retornar todas as unidades
    /// </summary>
    /// <returns></returns>
    [Authorize(Roles = "ADMINISTRATOR, MANAGER, SELLER")]
    [HttpGet("get-all")]
    public async Task<ActionResult<GetUnitResponse>> GetAllUnitSAsync(ISender mediator,
        CancellationToken cancellationToken = default)
    {
        return await mediator.Send(new GetUnitRequest(), cancellationToken);
    }
    #endregion

    #region Post
    /// <summary>
    /// Cadastrar Unidade
    /// </summary>
    /// <param name="mediator"></param>
    /// <param name="unitRequest"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [Authorize(Roles = "ADMINISTRATOR, MANAGER")]
    [HttpPost("insert-unit")]
    public async Task<ActionResult<CreateUnitResponse>> InsertUnitAsync(ISender mediator,
        CreateUnitRequest unitRequest,
        CancellationToken cancellationToken = default)
    {
        return await mediator.Send(unitRequest, cancellationToken);
    }
    #endregion

    #region Put
    /// <summary>
    /// Atualizar unidade
    /// </summary>
    /// <param name="mediator"></param>
    /// <param name="unitRequest"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [Authorize(Roles = "ADMINISTRATOR, MANAGER")]
    [HttpPut("update-unit")]
    public async Task<bool> UpdateUnitAsync(ISender mediator,
        UpdateUnitRequest unitRequest,
        CancellationToken cancellationToken = default)
    {
        return await mediator.Send(unitRequest, cancellationToken);

    }
    #endregion

    #region Delete
    /// <summary>
    /// Ecluir Unidade
    /// </summary>
    /// <param name="mediator"></param>
    /// <param name="unitRequest"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [Authorize(Roles = "ADMINISTRATOR, MANAGER")]
    [HttpDelete("delete-unit")]
    public async Task<bool> DeleteUnitAsync(ISender mediator,
        DeleteUnitRequest unitRequest,
        CancellationToken cancellationToken = default)
    {
        return await mediator.Send(unitRequest, cancellationToken);
    }
    #endregion
}
