using CashFlow.Domain.Aggregates.CashFlow.Entities;
using CashFlow.Worker.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;


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
    
    public async Task<DailyConsolidatedBalance?> GetByDateAsync(DateOnly date)
    {
        return await _context.DailyConsolidatedBalances
            .FirstOrDefaultAsync(d => d.Date == date);
    }

    public async Task AddAsync(DailyConsolidatedBalance entity)
    {
        await _context.DailyConsolidatedBalances.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(DailyConsolidatedBalance entity)
    {
        _context.DailyConsolidatedBalances.Update(entity);
        await _context.SaveChangesAsync();
    }
}