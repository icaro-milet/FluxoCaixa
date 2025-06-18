namespace CashFlow.Application.DTOs;

public class CreateTransactionRequestDTO
{
    public decimal Amount { get; set; }
    
    public string Type { get; set; } = null!;
    
    public string Description { get; set; } = string.Empty;
    
    public DateTime Date { get; set; }
}