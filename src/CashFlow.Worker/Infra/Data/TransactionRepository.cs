using CashFlow.Domain.Aggregates.CashFlow.Entities;
using CashFlow.Worker.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;


namespace CashFlow.Worker.Infra.Data;

public class TransactionRepository : ITransactionRepository
{
    private readonly WorkerDbContext _context;

    public TransactionRepository(WorkerDbContext context)
    {
        _context = context;
    }

    public async Task<List<Transaction>> GetTransactionsByDateAsync(DateOnly date)
        {
        IQueryable<Transaction> query = _context.Transactions;
        
        var result = await query
            .Where(t => t.Date.Equals(date))
            .ToListAsync();

        return result;
    }
}