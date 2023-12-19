﻿using Desafio.Domain;

namespace Desafio.Infrastructure;

public interface IPersonRepository
{
    Task InsertAsync(Person product);
    Task UpdateAsync(Person product);
    Task RemoveAsync(int id);
    Task<Person> GetByIdAsync(int id);
    Task<Person> GetAllAsync();
}
