using CashFlow.Worker.Application.Interfaces;

namespace CashFlow.Worker.Services;

public class ConsolidationWorker : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<ConsolidationWorker> _logger;

    public ConsolidationWorker(IServiceProvider serviceProvider, ILogger<ConsolidationWorker> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("ConsolidationWorker iniciado.");

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                using var scope = _serviceProvider.CreateScope();
                var appService = scope.ServiceProvider.GetRequiredService<IDailyConsolidationAppService>();

                await appService.ProcessDailyConsolidationAsync();

                await Task.Delay(TimeSpan.FromHours(24), stoppingToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao processar consolidação diária.");
            }
        }
    }
}