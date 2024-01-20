using AutoMapper;
using Desafio.Domain;

namespace Desafio.Application;

public class UnitService : IUnitService
{
    private readonly IUnitRepository _unitRepository;
    private readonly IMapper _mapper;

    public UnitService(IUnitRepository unitRepository, IMapper mapper) 
    {
        _unitRepository = unitRepository;
        _mapper = mapper;
    }

    #region Controller Methods
    public async Task<IEnumerable<UnitResponse>> GetAllAsync()
    {
        var result = _mapper.Map<IEnumerable<UnitResponse>>(await _unitRepository.GetAllAsync());
        
        if (result == null)
        {
            throw new Exception("No products were found.");
        }
       
        return result;
    }

    public async Task<UnitResponse> GetByAcronymAsync(string acronym)
    {
        var unit = await _unitRepository.GetByAcronymAsync(acronym.ToUpper());

        if (unit == null)
        {
            throw new Exception("The unit was not found.");
        }

        return _mapper.Map<UnitResponse>(unit);
    }

    public async Task<UnitResponse> GetByShortIdAsync(string shortId)
    {
        var unit = await _unitRepository.GetByShortIdAsync(shortId);

        if (unit == null)
        {
            throw new Exception("No units were found.");
        }

        return _mapper.Map<UnitResponse>(unit);
    }

    public async Task<CreateUnitResponse> InsertAsync(CreateUnitRequest unitRequest)
    {
        var unit = _mapper.Map<Unit>(unitRequest);

        await _unitRepository.InsertAsync(unit);
        var newUnit = _mapper.Map<CreateUnitResponse>(unit);
        return newUnit;

    }

    public async Task<bool> RemoveAsync(string acronym)
    {
        var unit = await _unitRepository.GetByAcronymAsync(acronym);
        if (unit == null)
        {
            throw new Exception("The unit was not found");
        }

        await _unitRepository.RemoveAsync(acronym);

        return true;
    }

    public async Task<bool> UpdateAsync(UpdateUnitRequest unitRequest)
    {
        var existingUnit = await _unitRepository.GetByAcronymAsync(unitRequest.Acronym.ToUpper());

        if (existingUnit == null)
        {
            throw new Exception("Unit was not found.");
        }

        _mapper.Map(unitRequest, existingUnit);

        await _unitRepository.UpdateAsync(existingUnit);

        return true;

    }
    #endregion

    #region Validations Methods
    public async Task<bool> UnitDoesNotExistsAsync(string acronym)
    {
        return await _unitRepository.GetByAcronymAsync(acronym) == null;
    }
    public async Task<bool> HasNotBeenUsedBeforeAsync(string acronym)
    {
        return await _unitRepository.HasNotBeenUsedBeforeAsync(acronym);
    }
    #endregion
}
