namespace Desafio.Application;
public interface IUnitService
{
    Task<CreateUnitResponse> InsertAsync(CreateUnitRequest unitRequest);
    Task<bool> UpdateAsync(UpdateUnitRequest unitRequest);
    Task<bool> RemoveAsync(string shortId);
    Task<UnitResponse> GetByAcronymAsync(string acronym);
    Task<UnitResponse> GetByShortIdAsync(string shortId);
    Task<IEnumerable<UnitResponse>> GetAllAsync();
    Task<bool> AcronymAlreadyUsedAsync(string acronym);
    Task<bool> HasBeenUsedBeforeAsync(string shortId);
}
