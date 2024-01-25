﻿using Desafio.Domain;
using Desafio.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Desafio.Application;

public class UnitRepository : IUnitRepository
{

    private readonly AppDbContext _appDbContext;
    
    public UnitRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<List<Unit>> GetAllAsync()
    {
        return await _appDbContext.Units.AsNoTracking().ToListAsync();
    }

    public async Task<Unit> GetByAcronymAsync(string acronym)
    {
        return await _appDbContext.Units.AsNoTracking().SingleOrDefaultAsync(x => x.Acronym == acronym);
    }

    public async Task<Unit> GetByShortIdAsync(string shortId)
    {
        return await _appDbContext.Units.AsNoTracking().SingleOrDefaultAsync(x => x.ShortId == shortId);
    }

    public async Task InsertAsync(Unit unit)
    {
        try
        {
            await _appDbContext.Units.AddAsync(unit);
            await SaveChangesAsync();
        }
        catch (Exception)
        {
            throw new CustomException("Error while inserting unit");
        }
        
    }

    public async Task RemoveAsync(string shortId)
    {
        try
        {
            Unit unit = await GetByShortIdAsync(shortId);
            if(unit == null)
            {
                throw new CustomException($"The unit was not found.");
            }
            _appDbContext.Units.Remove(unit);
            await SaveChangesAsync();
        }
        catch (Exception)
        {
            throw; 
        }
        
    }

    public async Task<Unit> UpdateAsync(Unit unit)
    {
        try
        {
            _appDbContext.Update(unit);
            await SaveChangesAsync();
            return unit;
        }
        catch (Exception)
        { 
            throw new CustomException("Error while updating unit");
        }
        
    }
    public async Task<int> SaveChangesAsync()
    {
        try
        {
            return await _appDbContext.SaveChangesAsync();
        }
        catch (Exception)
        {
            throw new CustomException("Error while saving unit");
        }
    }

    public async Task<bool> HasBeenUsedBeforeAsync(string shortId)
    {
        return await _appDbContext.Products.AsNoTracking().AnyAsync(x => x.ShortId == shortId);
    }

}
