﻿using Desafio.Domain;
using Desafio.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Desafio.Application;

public class PersonRepository : IPersonRepository
{
    private readonly AppDbContext _appDbContext;

    public PersonRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<List<Person>> GetAllAsync()
    {
        return await _appDbContext.People.AsNoTracking().ToListAsync();
    }

    public async Task<List<Person>> GetAllClientAsync()
    {
        return await _appDbContext.People.AsNoTracking().Where(x => x.CanBuy).ToListAsync();
    }

    public async Task InsertAsync(Person person)
    {
        await _appDbContext.People.AddAsync(person);
        await SaveChangesAsync();
    }

    public async Task RemoveAsync(string shortId)
    {
        Person person = await GetByShortIdAsync(shortId);

        if (person == null)
        {
            throw new CustomException("The person was found.");
        }
        _appDbContext.People.Remove(person);
        await SaveChangesAsync();
    }

    public async Task<Person> UpdateAsync(Person person)
    {
        try
        {
            _appDbContext.Update(person);
            await SaveChangesAsync();
            return person;
        }
        catch (Exception)
        {
            throw new CustomException("Error while updating person");
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
            throw new CustomException("Error while saving person");
        }
    }

    public async Task<Person> GetByShortIdAsync(string shortId)
    {
        return await _appDbContext.People.AsNoTracking().SingleOrDefaultAsync(x => x.ShortId == shortId);
    }

    public async Task<bool> AlternativeCodeAlreadyExistsAsync(string alternativeCode)
    {
        return await _appDbContext.People.AsNoTracking().AnyAsync(x => x.AlternativeCode == alternativeCode);
    }
    public async Task<bool> AlternativeCodeAlreadyExistsAsync(UpdatePersonRequest request)
    {
        return await _appDbContext.People.AsNoTracking().AnyAsync(x => x.AlternativeCode == request.AlternativeCode && x.ShortId != request.ShortId);
    }

    public async Task<bool> DocumentAlreadyExistsAsync(string document)
    {
        return await _appDbContext.People.AsNoTracking().AnyAsync(x => x.Document == document);
    }
    public async Task<bool> DocumentAlreadyExistsAsync(UpdatePersonRequest request)
    {
        return await _appDbContext.People.AsNoTracking().AnyAsync(x => x.Document == request.Document && x.ShortId != request.ShortId);
    }

    public async Task<bool> PersonCanBuyAsync(string shortId)
    {
        Person person = await GetByShortIdAsync(shortId);
        return person.CanBuy;
    }
}
