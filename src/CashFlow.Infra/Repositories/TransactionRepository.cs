using CashFlow.Domain.Aggregates.CashFlow.Entities;
using CashFlow.Domain.Aggregates.CashFlow.Interfaces.Repositories;

namespace CashFlow.Infra.Repositories;

public class TransactionRepository : ITransactionRepository
{
    public readonly TransactionContext _transactionContext;

    public TransactionRepository(TransactionContext transactionContext)
    {
        _transactionContext = transactionContext;
    }
    
    public async Task<Domain.Aggregates.CashFlow.Entities.Transaction> AddAsync(Transaction transaction)
    {
        var result = await _transactionContext.AddAsync(transaction);
        await _transactionContext.SaveChangesAsync(); 
        
        return result.Entity;
    }

    public Task<IEnumerable<Transaction>> GetByDateAsync(DateOnly date)
    {
        throw new NotImplementedException();
    }
}