using System;
using System.Threading.Tasks;
using CashFlow.Domain.Aggregates.CashFlow.Entities;
using CashFlow.Worker.Application.Interfaces;
using MassTransit;

namespace CashFlow.Worker.Consumers;

public class TransactionCreatedConsumer : IConsumer<TransactionCreatedEvent>
{
    private readonly IDailyConsolidationAppService _dailyConsolidationAppService;

    public TransactionCreatedConsumer(IDailyConsolidationAppService dailyConsolidationAppService)
    {
        _dailyConsolidationAppService = dailyConsolidationAppService;
    }

    public async Task Consume(ConsumeContext<TransactionCreatedEvent> context)
    {
        var message = context.Message;
        Console.WriteLine($"[Worker] Evento recebido: {message.Id}");

        await _dailyConsolidationAppService.ProcessTransactionAsync(message);
    }
}