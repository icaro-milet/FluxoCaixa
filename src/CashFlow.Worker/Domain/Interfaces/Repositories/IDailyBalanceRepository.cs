using CashFlow.Domain.Aggregates.CashFlow.Entities;

namespace CashFlow.Worker.Domain.Interfaces.Repositories;

public interface IDailyBalanceRepository
{
    Task SaveDailyBalanceAsync(DailyConsolidatedBalance balance);
}