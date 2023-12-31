﻿using Microsoft.EntityFrameworkCore;
using Desafio.Domain;
using Desafio.Infrastructure.Mapping;

namespace Desafio.Infrastructure;

public class UserContext : DbContext
{
    #region DbSet
    public DbSet<User> Users => Set<User>();
    #endregion

    public UserContext(DbContextOptions<UserContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("public");
        modelBuilder.ApplyConfiguration(new UserMapping());
    }
}
