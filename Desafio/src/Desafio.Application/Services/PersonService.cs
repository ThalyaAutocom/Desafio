using AutoMapper;
using Desafio.Domain;

namespace Desafio.Application;

public class PersonService : IPersonService
{
    private readonly IPersonRepository _personRepository;
    private readonly IMapper _mapper;

    public PersonService(IPersonRepository personRepository, IMapper mapper) 
    {
        _personRepository = personRepository;
        _mapper = mapper;
    }

    #region Controller Methods
    public async Task<IEnumerable<PersonResponse>> GetAllAsync()
    {
        var result = _mapper.Map<IEnumerable<PersonResponse>>(await _personRepository.GetAllAsync());

        if(result == null || result.Count() == 0)
        {
            throw new Exception("No person was found");
        }

        return result;
    }
    public async Task<IEnumerable<PersonResponse>> GetAllClientAsync()
    {
        var result = _mapper.Map<IEnumerable<PersonResponse>>(await _personRepository.GetAllClientAsync());

        if (result == null || result.Count() == 0)
        {
            throw new Exception("No client was found");
        }

        return result;
    }

    public async Task<PersonResponse> GetByIdAsync(Guid id)
    {
        var person = await _personRepository.GetByIdAsync(id);

        if (person == null)
        {
            throw new Exception("No person was found.");
        }

        return _mapper.Map<PersonResponse>(person);
    }

    public async Task<PersonResponse> GetClientByIdAsync(Guid id)
    {
        var person = await _personRepository.GetClientByIdAsync(id);

        if (person == null)
        {
            throw new Exception("No client was found.");
        }

        return _mapper.Map<PersonResponse>(person);
    }

    public async Task<PersonResponse> GetByShortIdAsync(string shortId)
    {
        var person = await _personRepository.GetByShortIdAsync(shortId);

        if (person == null)
        {
            throw new Exception("No person was found.");
        }

        return _mapper.Map<PersonResponse>(person);
    }

    public async Task<PersonResponse> InsertAsync(CreatePersonRequest personRequest)
    {
        var person = _mapper.Map<Person>(personRequest);

        await _personRepository.InsertAsync(person);
        var newperson = _mapper.Map<PersonResponse>(person);
        return newperson;
    }

    public async Task<PersonResponse> RemoveAsync(Guid id)
    {
        var person = await _personRepository.GetByIdAsync(id);

        if (person == null)
        {
            throw new Exception("No person was found.");
        }

        await _personRepository.RemoveAsync(id);

        return null;
    }

    public async Task<PersonResponse> UpdateAsync(UpdatePersonRequest personRequest)
    {
        var existingperson = await _personRepository.GetByIdAsync(personRequest.Id);

        if (existingperson == null)
        {
            throw new Exception("No person was found.");
        }

        _mapper.Map<Person>(personRequest);

        _mapper.Map(personRequest, existingperson);
        await _personRepository.UpdateAsync(existingperson);

        var personResponse = _mapper.Map<PersonResponse>(existingperson);

        return personResponse;
    }
    #endregion

    #region Validation Methods
    public async Task<bool> DocumentAlreadyExistsAsync(string document)
    {
        return await _personRepository.DocumentAlreadyExistsAsync(document);
    }
    public async Task<bool> AlternativeCodeAlreadyExistsAsync(string alternativeCode)
    {
        return await _personRepository.AlternativeCodeAlreadyExistsAsync(alternativeCode);
    }
    public async Task<bool> PersonCanBuyAsync(Guid id)
    {
        return await _personRepository.PersonCanBuyAsync(id);
    }
    #endregion
}
