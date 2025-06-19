namespace CashFlow.Domain.Aggregates.CashFlow.Interfaces.Services;

public interface ITransactionService
{
    Task<Entities.Transaction> AddAsync(Domain.Aggregates.CashFlow.Entities.Transaction transaction);
}