using CashFlow.Domain.Aggregates.CashFlow.Entities;

namespace CashFlow.Domain.Aggregates.CashFlow.Interfaces.Repositories;

public interface ITransactionRepository
{
    Task AddAsync(Transaction transaction);
    Task<IEnumerable<Transaction>> GetByDateAsync(DateOnly date);
}