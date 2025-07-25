using CashFlow.Domain.Aggregates.CashFlow.Entities;
using CashFlow.Worker.Application.Interfaces;
using CashFlow.Worker.Domain.Interfaces.Repositories;

namespace CashFlow.Worker.Application.Services;

public class DailyConsolidationAppService : IDailyConsolidationAppService
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly IDailyBalanceRepository _dailyBalanceRepository;
    private readonly ILogger<DailyConsolidationAppService> _logger;

    public DailyConsolidationAppService(
        ITransactionRepository transactionRepository,
        IDailyBalanceRepository dailyBalanceRepository,
        ILogger<DailyConsolidationAppService> logger)
    {
        _transactionRepository = transactionRepository;
        _dailyBalanceRepository = dailyBalanceRepository;
        _logger = logger;
    }

    public async Task ProcessDailyConsolidationAsync()
    {
        var dateToConsolidate = DateOnly.FromDateTime(DateTime.Today);
        
        _logger.LogInformation($"Consolidando saldo para o dia {dateToConsolidate:yyyy-MM-dd}");

        var transactions = await _transactionRepository.GetTransactionsByDateAsync(dateToConsolidate);
        var total = transactions.Sum(t => t.Amount);

        var consolidatedBalance = new DailyConsolidatedBalance
        {
            Date = dateToConsolidate,
            ConsolidatedBalance = total
        };

        await _dailyBalanceRepository.SaveDailyBalanceAsync(consolidatedBalance);

        _logger.LogInformation($"Saldo consolidado: {total}");
    }
    
    public async Task ProcessTransactionAsync(TransactionCreatedEvent evt)
    {
        var date = DateOnly.FromDateTime(evt.Date);
        var currentBalance = await _dailyBalanceRepository.GetByDateAsync(date);

        if (currentBalance == null)
        {
            currentBalance = new DailyConsolidatedBalance
            {
                Date = date,
                ConsolidatedBalance = evt.Amount
            };
            await _dailyBalanceRepository.SaveDailyBalanceAsync(currentBalance);
        }
        else
        {
            currentBalance.ConsolidatedBalance += evt.Amount;
            await _dailyBalanceRepository.UpdateAsync(currentBalance);
        }

        _logger.LogInformation($"Saldo consolidado atualizado para {date}: {currentBalance.ConsolidatedBalance}");
    }
}