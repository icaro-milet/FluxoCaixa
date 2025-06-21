namespace CashFlow.Application.DTOs;

public class TransactionCreatedEventDTO
{
    public Guid Id { get; set; }
    public decimal Amount { get; set; }
    public string Type { get; set; } 
    public string Description { get; set; }
    public DateTime Date { get; set; } 
}