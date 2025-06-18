using CashFlow.Domain.Aggregates.CashFlow.Entities;
using CashFlow.Domain.Aggregates.CashFlow.Interfaces.Services;

namespace CashFlow.Domain.Aggregates.CashFlow.Services;

public class TransactionService : ITransactionService
{
    public Task AddAsync(Transaction transaction)
    {
        throw new NotImplementedException();
    }
}