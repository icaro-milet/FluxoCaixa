using CashFlow.Infra.Configurations;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Infra;

public class TransactionContext : DbContext
{
    public TransactionContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Domain.Aggregates.CashFlow.Entities.Transaction>? Transactions { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        new TransactionEntityConfiguration().Configure(modelBuilder.Entity<Domain.Aggregates.CashFlow.Entities.Transaction>());
            
    }
}