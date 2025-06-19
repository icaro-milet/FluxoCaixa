using CashFlow.Domain.Aggregates.CashFlow.Entities;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Worker.Infra;

public class WorkerDbContext : DbContext
{
    public WorkerDbContext(DbContextOptions<WorkerDbContext> options)
        : base(options)
    {
    }

    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<DailyConsolidatedBalance> DailyConsolidatedBalances { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DailyConsolidatedBalance>()
            .HasKey(d => d.Date);
        modelBuilder.Entity<DailyConsolidatedBalance>()
            .ToTable("DailyConsolidatedBalances");

        base.OnModelCreating(modelBuilder);
    }
}