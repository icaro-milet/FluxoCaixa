using CashFlow.Domain.Aggregates.CashFlow.ValueObjects;

namespace CashFlow.Domain.Aggregates.CashFlow.Entities;

public class Transaction
{
    public Guid Id { get; private set; }
    public decimal Amount { get; set; }
    public TransactionType Type { get; private set; }
    public string? Description { get; private set; }
    public DateOnly Date { get; private set; }

    public Transaction(decimal amount, TransactionType type, string? description, DateOnly date)
    {
        Id = Guid.NewGuid();
        Amount = amount;
        Type = type;
        Description = description;
        Date = date;
    }
}