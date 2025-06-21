using System.Threading.Tasks;
using CashFlow.Domain.Aggregates.CashFlow.Entities;

namespace CashFlow.Worker.Application.Interfaces;

public interface IDailyConsolidationAppService
{
    Task ProcessDailyConsolidationAsync();
    Task ProcessTransactionAsync(TransactionCreatedEvent evt);
}