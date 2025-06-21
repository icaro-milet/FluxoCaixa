using CashFlow.Domain.Aggregates.CashFlow.Entities;
using CashFlow.Domain.Aggregates.CashFlow.Interfaces.Repositories;
using MassTransit;
using MassTransit.Transports;

namespace CashFlow.Infra.Repositories;

public class TransactionRepository : ITransactionRepository
{
    private readonly TransactionContext _context;
    private readonly IPublishEndpoint _publishEndpoint;

    public TransactionRepository(
        TransactionContext context,
        IPublishEndpoint publishEndpoint)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _publishEndpoint = publishEndpoint ?? throw new ArgumentNullException(nameof(publishEndpoint));
    }
    
    public async Task<Domain.Aggregates.CashFlow.Entities.Transaction> AddAsync(Transaction transaction)
    {
        if (transaction is null)
            throw new ArgumentNullException(nameof(transaction));

        var result = await _context.AddAsync(transaction);
        await _context.SaveChangesAsync(); 
        
        await PublishTransactionCreatedAsync(transaction);

        return result.Entity;
    }

    private Task PublishTransactionCreatedAsync(Transaction transaction)
    {
        var @event = new TransactionCreatedEvent
        {
            Id = transaction.Id,
            Amount = transaction.Amount,
            Date = transaction.Date.ToDateTime(TimeOnly.MinValue),
            Description = transaction.Description
        };
        
        Console.WriteLine("📤 Publicando evento para o RabbitMQ...");
        var result = _publishEndpoint.Publish(@event);
        Console.WriteLine("✅ Evento publicado.");
        
        return result;
    }
}