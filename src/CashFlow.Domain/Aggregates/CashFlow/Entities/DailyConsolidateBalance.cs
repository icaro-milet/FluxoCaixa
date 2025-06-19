namespace CashFlow.Domain.Aggregates.CashFlow.Entities;

public class DailyConsolidatedBalance
{
    public DateTime Date { get; set; }
    public decimal ConsolidatedBalance { get; set; }
}