﻿using GestioneSagre.Utility.Infrastructure.DataAccess.Mapper;
using GestioneSagre.Utility.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace GestioneSagre.Utility.Infrastructure.DataAccess;

public class ReadDbContext : DbContext
{
    public ReadDbContext(DbContextOptions<ReadDbContext> options) : base(options)
    {
    }

    public virtual DbSet<EmailMessage> EmailMessages { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new EmailMessageMapper());
    }
}