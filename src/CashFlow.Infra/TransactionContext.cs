using CashFlow.Domain.Aggregates.CashFlow.Entities;
using CashFlow.Infra.Configurations;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Infra;

public class TransactionContext : DbContext
{
    public TransactionContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Domain.Aggregates.CashFlow.Entities.Transaction>? Transactions { get; set; }

    public DbSet<DailyConsolidatedBalance> DailyConsolidatedBalances { get; set; }    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        new TransactionEntityConfiguration().Configure(modelBuilder.Entity<Domain.Aggregates.CashFlow.Entities.Transaction>()); 
        
        modelBuilder.Entity<Transaction>()
            .Property(t => t.Type)
            .HasConversion<string>();
        
        modelBuilder.Entity<DailyConsolidatedBalance>()
            .HasKey(d => d.Date);
        modelBuilder.Entity<DailyConsolidatedBalance>()
            .ToTable("DailyConsolidatedBalances");
    }
    
    
}