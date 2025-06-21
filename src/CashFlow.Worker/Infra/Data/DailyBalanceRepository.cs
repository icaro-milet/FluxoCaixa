using CashFlow.Domain.Aggregates.CashFlow.Entities;
using CashFlow.Worker.Domain.Interfaces.Repositories;


namespace CashFlow.Worker.Infra.Data;

public class DailyBalanceRepository : IDailyBalanceRepository
{
    private readonly WorkerDbContext _context;

    public DailyBalanceRepository(WorkerDbContext context)
    {
        _context = context;
    }

    public async Task SaveDailyBalanceAsync(DailyConsolidatedBalance balance)
    {
        var existing = await _context.DailyConsolidatedBalances.FindAsync(balance.Date);
        if (existing == null)
        {
            await _context.DailyConsolidatedBalances.AddAsync(balance);
            await _context.SaveChangesAsync();
        }
        else
            existing.ConsolidatedBalance = balance.ConsolidatedBalance;

        await _context.SaveChangesAsync();
    }
}