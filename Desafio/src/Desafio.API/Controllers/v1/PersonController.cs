using Desafio.Application;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Desafio.API;
[ApiVersion("1.0")]
public class PersonController : DesafioControllerBase
{
    public PersonController()
    {
        
    }

    #region Get
    /// <summary>
    /// Retornar todas as pessoas
    /// </summary>
    /// <returns></returns>
    [Authorize(Roles = "ADMINISTRATOR, MANAGER, SELLER")]
    [HttpGet("get-all-person")]
    public async Task<ActionResult<GetPersonResponse>> GetAllPerson(ISender mediator,
        bool? enable,
        CancellationToken cancellationToken = default)
    {
        return await mediator.Send(new GetPersonRequest(enable), cancellationToken);
    }

    /// <summary>
    /// Retornar pessoa por Short Id
    /// </summary>
    /// <param name="mediator"></param>
    /// <param name="shortId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [Authorize(Roles = "ADMINISTRATOR, MANAGER, SELLER")]
    [HttpGet("get-by-short-id")]
    public async Task<ActionResult<PersonResponse>> GetPersonByShortIdAsync(ISender mediator,
        string shortId,
        CancellationToken cancellationToken = default)
    {
        return await mediator.Send(new GetByShortIdPersonRequest(shortId), cancellationToken);
    }

    /// <summary>
    /// Retornar todos os clientes
    /// </summary>
    /// <returns></returns>
    [Authorize(Roles = "ADMINISTRATOR, MANAGER, SELLER")]
    [HttpGet("get-all-clients")]
    public async Task<ActionResult<GetPersonResponse>> GetAllClients(ISender mediator,
        bool? enable,
        CancellationToken cancellationToken = default)
    {
        return await mediator.Send(new GetClientRequest(enable), cancellationToken);
    }
    #endregion

    #region Post
    /// <summary>
    /// Cadastrar Pessoa
    /// </summary>
    /// <param name="mediator"></param>
    /// <param name="personRequest"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [Authorize(Roles = "ADMINISTRATOR, MANAGER")]
    [HttpPost("create-person")]
    public async Task<ActionResult<CreatePersonResponse>> CreatePersonAsync(ISender mediator,
        CreatePersonRequest personRequest,
        CancellationToken cancellationToken = default)
    {
        return await mediator.Send(personRequest, cancellationToken);
    }
    #endregion

    #region Put

    /// <summary>
    /// Atualizar pessoa
    /// </summary>
    /// <param name="mediator"></param>
    /// <param name="personRequest"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [Authorize(Roles = "ADMINISTRATOR, MANAGER")]
    [HttpPut("update-person")]
    public async Task<ActionResult<bool>> UpdatePersonAsync(ISender mediator,
        UpdatePersonRequest personRequest,
        CancellationToken cancellationToken = default)
    {
        return await mediator.Send(personRequest, cancellationToken);
    }
    #endregion

    #region Delete
    /// <summary>
    /// Excluir Pessoa
    /// </summary>
    /// <param name="mediator"></param>
    /// <param name="personRequest"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [Authorize(Roles = "ADMINISTRATOR, MANAGER")]
    [HttpDelete("delete-person")]
    public async Task<ActionResult<bool>> RemovePersonAsync(ISender mediator,
        DeletePersonRequest personRequest,
        CancellationToken cancellationToken = default)
    {
        return await mediator.Send(personRequest, cancellationToken);
    }
    #endregion
}
