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

    public async Task<List<Transaction>> GetTransactionsByDateAsync(DateTime date)
    {
        var startOfDayUtc = date.Date.ToUniversalTime();
        var endOfDayUtc = startOfDayUtc.AddDays(1);

        IQueryable<Transaction> query = _context.Transactions;

        var result = await query
            .Where(t => t.Date >= startOfDayUtc && t.Date < endOfDayUtc)
            .ToListAsync();

        return result;
    }
}