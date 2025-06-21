using CashFlow.Domain.Aggregates.CashFlow.Entities;

namespace CashFlow.Worker.Domain.Interfaces.Repositories;

public interface IDailyBalanceRepository
{
    Task SaveDailyBalanceAsync(DailyConsolidatedBalance balance);
    Task AddAsync(DailyConsolidatedBalance entity);
    Task UpdateAsync(DailyConsolidatedBalance entity);
    Task<DailyConsolidatedBalance?> GetByDateAsync(DateOnly date);
}