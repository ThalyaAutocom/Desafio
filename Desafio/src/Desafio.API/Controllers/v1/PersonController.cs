using Desafio.Application;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Desafio.API;
[ApiVersion("1.0")]
public class PersonController : DesafioControllerBase
{
    private readonly IPersonService _personService;

    public PersonController(IPersonService personService)
    {
        _personService = personService;
    }

    #region Get
    /// <summary>
    /// Retornar pessoa por Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [Authorize(Roles = "ADMINISTRATOR, MANAGER, SELLER")]
    [HttpGet("get-person-by-id")]
    public async Task<ActionResult<PersonResponse>> GetPersonAsync(Guid id)
    {
        PersonResponse result = await _personService.GetByIdAsync(id);

        return null;
    }

    /// <summary>
    /// Retornar todas as pessoas
    /// </summary>
    /// <returns></returns>
    [Authorize(Roles = "ADMINISTRATOR, MANAGER, SELLER")]
    [HttpGet("get-all-person")]
    public async Task<ActionResult<PersonResponse>> GetAllPerson()
    {
        IEnumerable<PersonResponse> result = await _personService.GetAllAsync();

        return null;

    }
    /// <summary>
    /// Retornar pessoa por Short Id
    /// </summary>
    /// <param name="shortId"></param>
    /// <returns></returns>
    [Authorize(Roles = "ADMINISTRATOR, MANAGER, SELLER")]
    [HttpGet("get-by-short-id")]
    public async Task<ActionResult<PersonResponse>> GetPersonByShortIdAsync(string shortId)
    {
        PersonResponse result = await _personService.GetByShortIdAsync(shortId);

        return null;
    }

    /// <summary>
    /// Retornar todos os clientes
    /// </summary>
    /// <returns></returns>
    [Authorize(Roles = "ADMINISTRATOR, MANAGER, SELLER")]
    [HttpGet("get-all-clients")]
    public async Task<ActionResult<PersonResponse>> GetAllClients()
    {
        IEnumerable<PersonResponse> result = await _personService.GetAllClientAsync();

        return null;

    }

    /// <summary>
    /// Retornar cliente por Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [Authorize(Roles = "ADMINISTRATOR, MANAGER, SELLER")]
    [HttpGet("get-client-by-id")]
    public async Task<ActionResult<PersonResponse>> GetClientAsync(Guid id)
    {
        PersonResponse result = await _personService.GetClientByIdAsync(id);

        return null;
    }
    #endregion

    #region Post
    /// <summary>
    /// Cadastrar Pessoa
    /// </summary>
    /// <param name="personRequest"></param>
    /// <returns></returns>
    [Authorize(Roles = "ADMINISTRATOR, MANAGER")]
    [HttpPost("insert-person")]
    public async Task<ActionResult<PersonResponse>> InsertPersonAsync(InsertPersonRequest personRequest)
    {
        PersonResponse result = await _personService.InsertAsync(personRequest);

        return null;
    }
    #endregion

    #region Put

    /// <summary>
    /// Atualizar pessoa
    /// </summary>
    /// <param name="personRequest"></param>
    /// <returns></returns>
    [Authorize(Roles = "ADMINISTRATOR, MANAGER")]
    [HttpPut("update-person-information")]
    public async Task<ActionResult<PersonResponse>> UpdatePersonAsync(UpdatePersonRequest personRequest)
    {
        PersonResponse result = await _personService.UpdateAsync(personRequest);

        return null;
    }
    #endregion

    #region Delete
    /// <summary>
    /// Excluir Pessoa
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [Authorize(Roles = "ADMINISTRATOR, MANAGER")]
    [HttpDelete("delete-person")]
    public async Task<ActionResult<PersonResponse>> RemovePersonAsync(Guid id)
    {
        PersonResponse result = await _personService.RemoveAsync(id);

        return null;
    }
    #endregion
}
