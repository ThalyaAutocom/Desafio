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
        
        if (result == null || result.Count() == 0)
        {
            throw new CustomException("No units were found.");
        }
       
        return result;
    }

    public async Task<UnitResponse> GetByAcronymAsync(string acronym)
    {
        if (acronym == null) throw new CustomException("The acronym was not provided.");
        
        var unit = await _unitRepository.GetByAcronymAsync(acronym.ToUpper());

        if (unit == null)
        {
            throw new CustomException("The unit was not found.");
        }

        return _mapper.Map<UnitResponse>(unit);
    }

    public async Task<UnitResponse> GetByShortIdAsync(string shortId)
    {
        if (shortId == null) throw new CustomException("The short id was not provided.");

        var unit = await _unitRepository.GetByShortIdAsync(shortId);

        if (unit == null)
        {
            throw new CustomException("The unit was not found.");
        }

        return _mapper.Map<UnitResponse>(unit);
    }

    public async Task<CreateUnitResponse> InsertAsync(CreateUnitRequest unitRequest)
    {
        if (unitRequest == null) throw new CustomException("The request was not provided.");

        var unit = _mapper.Map<Unit>(unitRequest);

        await _unitRepository.InsertAsync(unit);
        var newUnit = _mapper.Map<CreateUnitResponse>(unit);
        return newUnit;

    }

    public async Task<bool> RemoveAsync(string shortId)
    {
        if (shortId == null) throw new CustomException("The short id was not provided.");

        await _unitRepository.RemoveAsync(shortId);

        return true;
    }

    public async Task<bool> UpdateAsync(UpdateUnitRequest unitRequest)
    {
        if (unitRequest == null) throw new CustomException("The request was not provided.");

        var existingUnit = await _unitRepository.GetByAcronymAsync(unitRequest.Acronym.ToUpper());

        if (existingUnit == null)
        {
            throw new CustomException("The unit was not found.");
        }

        _mapper.Map(unitRequest, existingUnit);

        await _unitRepository.UpdateAsync(existingUnit);

        return true;

    }
    #endregion

    #region Validations Methods
    public async Task<bool> AcronymAlreadyUsedAsync(string acronym)
    {
        return await _unitRepository.GetByAcronymAsync(acronym) != null;
    }
    public async Task<bool> HasBeenUsedBeforeAsync(string shortId)
    {
        return await _unitRepository.HasBeenUsedBeforeAsync(shortId);
    }
    #endregion
}
