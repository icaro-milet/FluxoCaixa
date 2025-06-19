using CashFlow.Domain.Aggregates.CashFlow.Entities;
using CashFlow.Domain.Aggregates.CashFlow.Interfaces.Repositories;
using CashFlow.Domain.Aggregates.CashFlow.Interfaces.Services;

namespace CashFlow.Domain.Aggregates.CashFlow.Services;

public class TransactionService : ITransactionService
{
    private readonly ITransactionRepository  _transactionRepository;

    public TransactionService(ITransactionRepository transactionRepository)
    {
        _transactionRepository = transactionRepository;
    }
    
    public async Task<Transaction> AddAsync(Transaction transaction)
    {
        return await _transactionRepository.AddAsync(transaction);
    }
}