using System;
using System.Threading;
using System.Threading.Tasks;
using CashFlow.Worker.Application.Interfaces;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CashFlow.Worker;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IDailyConsolidationAppService _dailyConsolidationAppService;

    public Worker(ILogger<Worker> logger, IDailyConsolidationAppService dailyConsolidationAppService)
    {
        _logger = logger;
        _dailyConsolidationAppService = dailyConsolidationAppService;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("ConsolidationWorker iniciado.");

        try
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Consolidando saldo para o dia {date}", DateOnly.FromDateTime(DateTime.UtcNow));

                await _dailyConsolidationAppService.ProcessDailyConsolidationAsync();

                await Task.Delay(TimeSpan.FromHours(24), stoppingToken);
            }
        }
        catch (TaskCanceledException)
        {
            _logger.LogInformation("ConsolidationWorker cancelado.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao processar consolidação diária.");
        }
    }
}