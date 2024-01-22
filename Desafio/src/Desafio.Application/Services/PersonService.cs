using AutoMapper;
using Desafio.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Reflection.Metadata;
using System.Xml.Linq;

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

    public async Task<CreatePersonResponse> InsertAsync(CreatePersonRequest personRequest)
    {
        var person = _mapper.Map<Person>(personRequest);

        await _personRepository.InsertAsync(person);
        var newperson = _mapper.Map<CreatePersonResponse>(person);
        return newperson;
    }

    public async Task<bool> RemoveAsync(Guid id)
    {
        var person = await _personRepository.GetByIdAsync(id);

        if (person == null)
        {
            throw new Exception("No person was found.");
        }

        await _personRepository.RemoveAsync(id);

        return true;
    }

    public async Task<bool> UpdateAsync(UpdatePersonRequest personRequest)
    {
        var existingperson = await _personRepository.GetByIdAsync(personRequest.Id);

        if (existingperson == null)
        {
            throw new Exception("No person was found.");
        }

        _mapper.Map<Person>(personRequest);

        _mapper.Map(personRequest, existingperson);
        await _personRepository.UpdateAsync(existingperson);

        return true;
    }
    #endregion

    #region Validation Methods
    public async Task<bool> DocumentAlreadyExistsAsync(string document)
    {
        //Retornar validação como verdadeira se vazia
        if (string.IsNullOrWhiteSpace(document)) return true; 

        return await _personRepository.DocumentAlreadyExistsAsync(document);
    }
    public async Task<bool> DocumentAlreadyExistsAsync(UpdatePersonRequest userRequest)
    {
        //Retornar validação como verdadeira se vazia
        if (string.IsNullOrWhiteSpace(userRequest.Document)) return true;

        return await _personRepository.DocumentAlreadyExistsAsync(userRequest);
    }
    public async Task<bool> AlternativeCodeAlreadyExistsAsync(string alternativeCode)
    {
        //Retornar validação como verdadeira se vazia
        if (string.IsNullOrWhiteSpace(alternativeCode)) return true;
        return await _personRepository.AlternativeCodeAlreadyExistsAsync(alternativeCode);
    }
    public async Task<bool> AlternativeCodeAlreadyExistsAsync(UpdatePersonRequest userRequest)
    {
        //Retornar validação como verdadeira se vazia
        if (string.IsNullOrWhiteSpace(userRequest.AlternativeCode)) return true;

        return await _personRepository.AlternativeCodeAlreadyExistsAsync(userRequest);
    }
    public async Task<bool> PersonCanBuyAsync(Guid id)
    {
        return await _personRepository.PersonCanBuyAsync(id);
    }
    #endregion
}
