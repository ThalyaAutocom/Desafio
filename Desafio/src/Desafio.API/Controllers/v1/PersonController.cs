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
    /// Select Person By Id
    /// </summary>
    /// <remarks>Searchs an especific person on database, using their Id</remarks>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("get-person-by-id")]
    public async Task<ActionResult<PersonResponse>> GetPersonAsync(Guid id)
    {
        PersonResponse result = await _personService.GetByIdAsync(id);

        return CustomResponse(result);
    }

    /// <summary>
    /// Select all People
    /// </summary>
    /// <remarks>Searchs all people registered in database</remarks>
    /// <returns></returns>
    [HttpGet("get-all-person")]
    public async Task<ActionResult<PersonResponse>> GetAllPerson()
    {
        IEnumerable<PersonResponse> result = await _personService.GetAllAsync();

        return CustomResponseList(result);

    }
    /// <summary>
    /// Select Person by Short Id
    /// </summary>
    /// <remarks>Searchs an especific person on database, using their Short Id</remarks>
    /// <param name="shortId"></param>
    /// <returns></returns>
    [HttpGet("get-by-short-id")]
    public async Task<ActionResult<PersonResponse>> GetPersonByShortIdAsync(string shortId)
    {
        PersonResponse result = await _personService.GetByShortIdAsync(shortId);

        return CustomResponse(result);
    }

    /// <summary>
    /// Select all Clients
    /// </summary>
    /// <remarks>Searchs all clients registered in database. Clients are the ones who has the propertie CanBuy sets as true</remarks>
    /// <returns></returns>
    [HttpGet("get-all-clients")]
    public async Task<ActionResult<PersonResponse>> GetAllClients()
    {
        IEnumerable<PersonResponse> result = await _personService.GetAllClientAsync();

        return CustomResponseList(result);

    }

    /// <summary>
    /// Select Client by Id
    /// </summary>
    /// <remarks>Searchs an especific client on database, using their Id. Clients are the ones who has the propertie CanBuy sets as true</remarks>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("get-client-by-id")]
    public async Task<ActionResult<PersonResponse>> GetClientAsync(Guid id)
    {
        PersonResponse result = await _personService.GetClientByIdAsync(id);

        return CustomResponse(result);
    }
    #endregion

    #region Post
    /// <summary>
    /// Insert Person
    /// </summary>
    /// <remarks>Inserts one person on database</remarks>
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
    /// Update Person
    /// </summary>
    /// <remarks>Updates a person information</remarks>
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
    /// Update Enable Property
    /// </summary>
    /// <remarks>Updates only the Enable property</remarks>
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
    /// Update CanBuy property
    /// </summary>
    /// <remarks>Updates only the CanBuy property. This property indicates that the person is a client</remarks>
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
    /// Delete person
    /// </summary>
    /// <remarks>Removes an especific person from the database</remarks>
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
