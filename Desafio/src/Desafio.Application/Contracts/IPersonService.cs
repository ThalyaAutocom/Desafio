using Desafio.Domain;
using System.Threading.Tasks;

namespace Desafio.Application;
public interface IPersonService
{
    Task<CreatePersonResponse> InsertAsync(CreatePersonRequest personRequest);
    Task<bool> UpdateAsync(UpdatePersonRequest person);
    Task<bool> RemoveAsync(string shortId);
    Task<IEnumerable<PersonResponse>> GetAllAsync();
    Task<IEnumerable<PersonResponse>> GetAllClientAsync();
    Task<PersonResponse> GetByShortIdAsync(string shortId);
    Task<bool> AlternativeCodeAlreadyExistsAsync(string alternativeCode);
    Task<bool> AlternativeCodeAlreadyExistsAsync(UpdatePersonRequest userRequest);
    Task<bool> DocumentAlreadyExistsAsync(string document);
    Task<bool> DocumentAlreadyExistsAsync(UpdatePersonRequest userRequest);

    Task<bool> PersonCanBuyAsync(string shortId);
}
