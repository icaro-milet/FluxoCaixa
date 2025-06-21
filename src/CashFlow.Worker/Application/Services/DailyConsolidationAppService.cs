using System;
using System.Threading.Tasks;
using CashFlow.Domain.Aggregates.CashFlow.Entities;
using CashFlow.Worker.Application.Interfaces;
using CashFlow.Worker.Domain.Interfaces.Repositories;
using Microsoft.Extensions.Logging;

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
}