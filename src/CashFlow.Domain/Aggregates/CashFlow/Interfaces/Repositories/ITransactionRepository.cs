using CashFlow.Domain.Aggregates.CashFlow.Entities;

namespace CashFlow.Domain.Aggregates.CashFlow.Interfaces.Repositories;

public interface ITransactionRepository
{
    Task<Entities.Transaction> AddAsync(Transaction transaction);
    //Task<Entities.Transaction> PublishEvent(TransactionCreatedEvent transaction);
}