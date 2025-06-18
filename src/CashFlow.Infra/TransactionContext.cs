using Microsoft.EntityFrameworkCore;

namespace CashFlow.Infra;

public class TransactionContext : DbContext
{
    public TransactionContext(DbContextOptions options) : base(options)
    {
    }
}