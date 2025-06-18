using CashFlow.Domain.Aggregates.CashFlow.ValueObjects;

namespace CashFlow.Domain.Aggregates.CashFlow.Entities;

public class Transaction
{
    public Guid Id { get; private set; }
    
    public decimal Amount { get; private set; }
    
    public string Type { get; private set; } = null!;
    
    public string Description { get; private set; } = string.Empty;
    
    public DateTime Date { get; private set; }

    private Transaction() {}

    public static Transaction Create(decimal amount, string type, string description, DateTime date)
    {
        // if (!TransactionType.IsValid(type))
        //     throw new DomainException("Invalid transaction type.");
        //
        // if (amount <= 0)
        //     throw new DomainException("Amount must be greater than zero.");
    
        return new Transaction
        {
            Id = Guid.NewGuid(),
            Amount = amount,
            Type = type.ToLower(),
            Description = description,
            Date = date
        };
    }
}