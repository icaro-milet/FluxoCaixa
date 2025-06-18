namespace CashFlow.Application.DTOs;

public class DailySummaryResponseDTO
{
    public DateOnly Date { get; set; }
    public decimal TotalCredits { get; set; }
    public decimal TotalDebits { get; set; }
    
    public decimal Balance => TotalCredits - TotalDebits;
}