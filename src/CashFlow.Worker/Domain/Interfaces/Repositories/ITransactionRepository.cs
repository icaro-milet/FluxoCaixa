using Transaction = CashFlow.Domain.Aggregates.CashFlow.Entities.Transaction;

namespace CashFlow.Worker.Domain.Interfaces.Repositories;

public interface ITransactionRepository
{
    Task<List<Transaction>> GetTransactionsByDateAsync(DateOnly date);
}