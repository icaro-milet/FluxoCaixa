using CashFlow.Domain.Aggregates.CashFlow.Entities;
using CashFlow.Domain.Aggregates.CashFlow.Interfaces.Repositories;
using CashFlow.Domain.Aggregates.CashFlow.Interfaces.Services;
using CashFlow.Domain.Aggregates.CashFlow.ValueObjects;

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
        var isCredit = IsCredit(transaction);
        
        if (isCredit)
            transaction.Amount *= -1;
        
        return await _transactionRepository.AddAsync(transaction);
    }

    private bool IsCredit(Transaction transaction)
    {
        if (transaction.Type == TransactionType.Credit)
        {
            return true;
        }
        return false;
    }
}