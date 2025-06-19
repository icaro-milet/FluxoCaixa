using System.ComponentModel.DataAnnotations;
using CashFlow.Domain.Aggregates.CashFlow.ValueObjects;
using Swashbuckle.AspNetCore.Annotations;

namespace CashFlow.Application.DTOs;

public class CreateTransactionRequestDTO
{
    public CreateTransactionRequestDTO(decimal amount, TransactionType type, DateOnly date)
    {
        Amount = amount;
        Type = type;
        Date = date;
    }

    [Required]
    public decimal Amount { get; set; }
    
    [Required]
    [SwaggerSchema("Transaction type. Allowed values: 'credit' or 'debit'.")]
    public TransactionType Type { get; set; }
    
    public string Description { get; set; } = string.Empty;
    
    public DateOnly Date { get; set; }
}