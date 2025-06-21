using System;
using System.Threading.Tasks;
using CashFlow.Domain.Aggregates.CashFlow.Entities;
using MassTransit;

namespace CashFlow.Worker.Consumers;

public class TransactionCreatedConsumer: IConsumer<TransactionCreatedEvent>
{
    public async Task Consume(ConsumeContext<TransactionCreatedEvent> context)
    {
        var message = context.Message;
        Console.WriteLine($"[Worker] Transação recebida: {message.Id} - {message.Description}");
      
    }
}