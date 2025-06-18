namespace CashFlow.Domain.Aggregates.CashFlow.Interfaces.Services;

public interface ITransactionService
{
    Task AddAsync(Domain.Aggregates.CashFlow.Entities.Transaction transaction);
}