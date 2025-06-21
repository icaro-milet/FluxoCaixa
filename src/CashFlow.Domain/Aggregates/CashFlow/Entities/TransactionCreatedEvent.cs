namespace CashFlow.Domain.Aggregates.CashFlow.Entities;

public class TransactionCreatedEvent
{
    public Guid Id { get; set; }
    public decimal Amount { get; set; }
    
    public string Type { get; set; } 
    public string Description { get; set; }
    public DateTime Date { get; set; } 
}