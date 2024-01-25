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
            throw new CustomException("No person was found");
        }

        return result;
    }
    public async Task<IEnumerable<PersonResponse>> GetAllClientAsync()
    {
        var result = _mapper.Map<IEnumerable<PersonResponse>>(await _personRepository.GetAllClientAsync());

        if (result == null || result.Count() == 0)
        {
            throw new CustomException("No client was found");
        }

        return result;
    }

    public async Task<PersonResponse> GetByShortIdAsync(string shortId)
    {
        if (shortId == null) throw new CustomException("The short id was not provided.");

        var person = await _personRepository.GetByShortIdAsync(shortId);

        if (person == null)
        {
            throw new CustomException("No person was found.");
        }

        return _mapper.Map<PersonResponse>(person);
    }

    public async Task<CreatePersonResponse> InsertAsync(CreatePersonRequest personRequest)
    {
        if (personRequest == null) throw new CustomException("The request was not provided.");

        var person = _mapper.Map<Person>(personRequest);

        await _personRepository.InsertAsync(person);
        var newperson = _mapper.Map<CreatePersonResponse>(person);
        return newperson;
    }

    public async Task<bool> RemoveAsync(string shortId)
    {
        if (shortId == null) throw new CustomException("The short id was not provided.");

        await _personRepository.RemoveAsync(shortId);

        return true;
    }

    public async Task<bool> UpdateAsync(UpdatePersonRequest personRequest)
    {
        if (personRequest == null) throw new CustomException("The request was not provided.");

        var existingperson = await _personRepository.GetByShortIdAsync(personRequest.ShortId);

        if (existingperson == null)
        {
            throw new CustomException("No person was found.");
        }

        _mapper.Map(personRequest, existingperson);
        await _personRepository.UpdateAsync(existingperson);

        return true;
    }
    #endregion

    #region Validation Methods
    public async Task<bool> DocumentAlreadyExistsAsync(string document)
    {
        //Retornar validação como verdadeira se vazia
        if (string.IsNullOrWhiteSpace(document)) return false; 

        return await _personRepository.DocumentAlreadyExistsAsync(document);
    }
    public async Task<bool> DocumentAlreadyExistsAsync(UpdatePersonRequest userRequest)
    {
        //Retornar validação como verdadeira se vazia
        if (string.IsNullOrWhiteSpace(userRequest.Document)) return false;

        return await _personRepository.DocumentAlreadyExistsAsync(userRequest);
    }
    public async Task<bool> AlternativeCodeAlreadyExistsAsync(string alternativeCode)
    {
        //Retornar validação como verdadeira se vazia
        if (string.IsNullOrWhiteSpace(alternativeCode)) return false;
        return await _personRepository.AlternativeCodeAlreadyExistsAsync(alternativeCode);
    }
    public async Task<bool> AlternativeCodeAlreadyExistsAsync(UpdatePersonRequest userRequest)
    {
        //Retornar validação como verdadeira se vazia
        if (string.IsNullOrWhiteSpace(userRequest.AlternativeCode)) return false;

        return await _personRepository.AlternativeCodeAlreadyExistsAsync(userRequest);
    }
    public async Task<bool> PersonCanBuyAsync(string shortId)
    {
        return await _personRepository.PersonCanBuyAsync(shortId);
    }
    #endregion
}
