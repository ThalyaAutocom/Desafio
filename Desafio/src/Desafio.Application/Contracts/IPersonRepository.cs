using Desafio.Domain;

namespace Desafio.Application;

public interface IPersonRepository
{
    Task InsertAsync(Person person);
    Task<Person> UpdateAsync(Person person);
    Task RemoveAsync(string shortId);
    Task<List<Person>> GetAllAsync();
    Task<List<Person>> GetAllClientAsync();
    Task<int> SaveChangesAsync();
    Task<Person> GetByShortIdAsync(string shortId);
    Task<bool> AlternativeCodeAlreadyExistsAsync(string alternativeCode);
    Task<bool> AlternativeCodeAlreadyExistsAsync(UpdatePersonRequest request);
    Task<bool> DocumentAlreadyExistsAsync(string document);
    Task<bool> DocumentAlreadyExistsAsync(UpdatePersonRequest request);
    Task<bool> PersonCanBuyAsync(string shortId);
}
