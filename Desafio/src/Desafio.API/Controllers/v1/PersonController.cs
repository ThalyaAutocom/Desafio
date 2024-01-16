using Desafio.Application;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Desafio.API;
[ApiVersion("1.0")]
public class PersonController : DesafioControllerBase
{
    private readonly IPersonService _personService;

    public PersonController(IPersonService personService, IError error) : base(error)
    {
        _personService = personService;
    }

    #region Get
    /// <summary>
    /// Retornar pessoa por Id
    /// </summary>
    /// <remarks>Retorna uma pessoa específica, pesquisando por seu Id</remarks>
    /// <param name="id"></param>
    /// <returns></returns>
    [Authorize(Roles = "ADMINISTRATOR, MANAGER, SELLER")]
    [HttpGet("get-person-by-id")]
    public async Task<ActionResult<PersonResponse>> GetPersonAsync(Guid id)
    {
        PersonResponse result = await _personService.GetByIdAsync(id);

        return CustomResponse(result);
    }

    /// <summary>
    /// Retornar todas as pessoas
    /// </summary>
    /// <remarks>Retorna todas as pessoas cadastradas</remarks>
    /// <returns></returns>
    [Authorize(Roles = "ADMINISTRATOR, MANAGER, SELLER")]
    [HttpGet("get-all-person")]
    public async Task<ActionResult<PersonResponse>> GetAllPerson()
    {
        IEnumerable<PersonResponse> result = await _personService.GetAllAsync();

        return CustomResponseList(result);

    }
    /// <summary>
    /// Retornar pessoa por Short Id
    /// </summary>
    /// <remarks>Retorna uma pessoa específica, pesquisando por seu Short Id</remarks>
    /// <param name="shortId"></param>
    /// <returns></returns>
    [Authorize(Roles = "ADMINISTRATOR, MANAGER, SELLER")]
    [HttpGet("get-by-short-id")]
    public async Task<ActionResult<PersonResponse>> GetPersonByShortIdAsync(string shortId)
    {
        PersonResponse result = await _personService.GetByShortIdAsync(shortId);

        return CustomResponse(result);
    }

    /// <summary>
    /// Retornar todos os clientes
    /// </summary>
    /// <remarks>Retorna todas as pessoas cadastradas. Clientes são todas as pessoas que possuem a propriedade "CanBuy" setadas como "true"</remarks>
    /// <returns></returns>
    [Authorize(Roles = "ADMINISTRATOR, MANAGER, SELLER")]
    [HttpGet("get-all-clients")]
    public async Task<ActionResult<PersonResponse>> GetAllClients()
    {
        IEnumerable<PersonResponse> result = await _personService.GetAllClientAsync();

        return CustomResponseList(result);

    }

    /// <summary>
    /// Retornar cliente por Id
    /// </summary>
    /// <remarks>Retorna uma pessoa específica, pesquisando por seu Id. Clientes são todas as pessoas que possuem a propriedade "CanBuy" setadas como "true"</remarks>
    /// <param name="id"></param>
    /// <returns></returns>
    [Authorize(Roles = "ADMINISTRATOR, MANAGER, SELLER")]
    [HttpGet("get-client-by-id")]
    public async Task<ActionResult<PersonResponse>> GetClientAsync(Guid id)
    {
        PersonResponse result = await _personService.GetClientByIdAsync(id);

        return CustomResponse(result);
    }
    #endregion

    #region Post
    /// <summary>
    /// Cadastrar Pessoa
    /// </summary>
    /// <remarks>Cadastra uma pessoa</remarks>
    /// <param name="personRequest"></param>
    /// <returns></returns>
    [Authorize(Roles = "ADMINISTRATOR, MANAGER")]
    [HttpPost("insert-person")]
    public async Task<ActionResult<PersonResponse>> InsertPersonAsync(InsertPersonRequest personRequest)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        PersonResponse result = await _personService.InsertAsync(personRequest);

        return CustomResponse(result);
    }
    #endregion

    #region Put

    /// <summary>
    /// Atualizar pessoa
    /// </summary>
    /// <remarks>Atualiza informações de uma pessoa</remarks>
    /// <param name="personRequest"></param>
    /// <returns></returns>
    [Authorize(Roles = "ADMINISTRATOR, MANAGER")]
    [HttpPut("update-person-information")]
    public async Task<ActionResult<PersonResponse>> UpdatePersonAsync(PersonRequest personRequest)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        PersonResponse result = await _personService.UpdateAsync(personRequest);

        return CustomResponse(result);
    }

    /// <summary>
    /// Atualizar propriedade "Enable" (Ativo)
    /// </summary>
    /// <remarks>Atualiza somente a propriedade "enable", indicando se a pessoa está ativa ou não</remarks>
    /// <param name="personRequest"></param>
    /// <returns></returns>
    [Authorize(Roles = "ADMINISTRATOR, MANAGER")]
    [HttpPut("update-enabled-property")]
    public async Task<ActionResult<bool>> UpdatePersonEnabled(EnabledPersonRequest personRequest)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        PersonResponse result = await _personService.UpdateEnablePersonAsync(personRequest);

        return CustomResponse(result);
    }

    /// <summary>
    /// Atualizar propriedade "CanBuy" (Cliente)
    /// </summary>
    /// <remarks>Atualiza somente a propriedade "CanBuy", indicando que a pessoa é do tipo Cliente</remarks>
    /// <param name="personRequest"></param>
    /// <returns></returns>
    [Authorize(Roles = "ADMINISTRATOR, MANAGER")]
    [HttpPut("update-canbuy-property")]
    public async Task<ActionResult<bool>> UpdatePersonSellable(CanBuyPersonRequest personRequest)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        PersonResponse result = await _personService.UpdateCanBuyPersonAsync(personRequest);

        return CustomResponse(result);
    }
    #endregion

    #region Delete
    /// <summary>
    /// Excluir Pessoa
    /// </summary>
    /// <remarks>Exclui um cadastro de pessoa</remarks>
    /// <param name="id"></param>
    /// <returns></returns>
    [Authorize(Roles = "ADMINISTRATOR, MANAGER")]
    [HttpDelete("delete-person")]
    public async Task<ActionResult<PersonResponse>> RemovePersonAsync(Guid id)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        PersonResponse result = await _personService.RemoveAsync(id);

        return CustomResponse(result);
    }
    #endregion
}
