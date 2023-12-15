﻿using Microsoft.EntityFrameworkCore;
using Desafio.Domain;

namespace Desafio.Infrastructure;

public class Context : DbContext
{
    #region DbSet
    public DbSet<Unit> Units => Set<Unit>();
    public DbSet<Person> People => Set<Person>();
    public DbSet<Product> Products => Set<Product>();
    #endregion

    public Context(DbContextOptions<Context> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("public");
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserContext).Assembly);
    }
}
