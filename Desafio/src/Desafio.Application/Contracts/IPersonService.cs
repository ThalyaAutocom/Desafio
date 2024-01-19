using Desafio.Domain;
using System.Threading.Tasks;

namespace Desafio.Application;
public interface IPersonService
{
    Task<PersonResponse> InsertAsync(CreatePersonRequest personRequest);
    Task<PersonResponse> UpdateAsync(UpdatePersonRequest person);
    Task<PersonResponse> RemoveAsync(Guid id);
    Task<PersonResponse> GetByIdAsync(Guid id);
    Task<PersonResponse> GetClientByIdAsync(Guid id);
    Task<IEnumerable<PersonResponse>> GetAllAsync();
    Task<IEnumerable<PersonResponse>> GetAllClientAsync();
    Task<PersonResponse> GetByShortIdAsync(string shortId);
    Task<bool> AlternativeCodeAlreadyExistsAsync(string alternativeCode);
    Task<bool> DocumentAlreadyExistsAsync(string document);
    Task<bool> PersonCanBuyAsync(Guid id);
}
