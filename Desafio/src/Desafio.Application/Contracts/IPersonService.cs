﻿using Desafio.Domain;
using System.Threading.Tasks;

namespace Desafio.Application;
public interface IPersonService
{
    Task<CreatePersonResponse> InsertAsync(CreatePersonRequest personRequest);
    Task<bool> UpdateAsync(UpdatePersonRequest person);
    Task<bool> RemoveAsync(Guid id);
    Task<PersonResponse> GetByIdAsync(Guid id);
    Task<PersonResponse> GetClientByIdAsync(Guid id);
    Task<IEnumerable<PersonResponse>> GetAllAsync();
    Task<IEnumerable<PersonResponse>> GetAllClientAsync();
    Task<PersonResponse> GetByShortIdAsync(string shortId);
    Task<bool> AlternativeCodeAlreadyExistsAsync(string alternativeCode);
    Task<bool> DocumentAlreadyExistsAsync(string document);
    Task<bool> PersonCanBuyAsync(Guid id);
}
