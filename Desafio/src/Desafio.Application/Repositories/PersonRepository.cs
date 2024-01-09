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
        return await _appDbContext.People.ToListAsync();
    }

    public async Task<Person> GetByIdAsync(Guid id)
    {
        return await _appDbContext.People.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task InsertAsync(Person person)
    {
        person.Id = Guid.NewGuid();
        await _appDbContext.People.AddAsync(person);
        await SaveChangesAsync();
    }

    public async Task RemoveAsync(Guid id)
    {
        Person person = await GetByIdAsync(id);

        if (person == null)
        {
            throw new Exception($"Person {id} doesn't exists.");
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
            throw new Exception("Error while updating person");
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
            throw new Exception("Error while saving person");
        }
    }

    public async Task<Person> GetByShortIdAsync(string shortId)
    {
        return await _appDbContext.People.FirstOrDefaultAsync(x => x.ShortId == shortId);
    }
}
