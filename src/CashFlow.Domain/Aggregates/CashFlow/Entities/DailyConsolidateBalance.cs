namespace CashFlow.Domain.Aggregates.CashFlow.Entities;

public class DailyConsolidatedBalance
{
    public DateOnly Date { get; set; }
    public decimal ConsolidatedBalance { get; set; }
}