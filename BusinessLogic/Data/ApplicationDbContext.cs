using System;
using System.Collections.Generic;
using BusinessLogic.Models;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic.Data;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Calculation> Calculations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Calculation>(entity =>
        {
            entity.Property(e => e.CreatDate).HasColumnType("datetime");
            entity.Property(e => e.ResCalculation).HasMaxLength(50);
            entity.Property(e => e.TypeCalculation).HasMaxLength(50);
            entity.Property(e => e.Value1Calculation).HasMaxLength(50);
            entity.Property(e => e.Value2Calculation).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
