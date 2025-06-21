using System.Threading.Tasks;

namespace CashFlow.Worker.Application.Interfaces;

public interface IDailyConsolidationAppService
{
    Task ProcessDailyConsolidationAsync();
}