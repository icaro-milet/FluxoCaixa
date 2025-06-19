using Microsoft.EntityFrameworkCore;

namespace CashFlow.Infra;

public class TransactionContext : DbContext
{
    public TransactionContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Domain.Aggregates.CashFlow.Entities.Transaction>? Transactions { get; set; }
}