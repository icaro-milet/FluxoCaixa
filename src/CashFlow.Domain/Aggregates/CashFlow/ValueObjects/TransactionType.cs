namespace CashFlow.Domain.Aggregates.CashFlow.ValueObjects;

public class TransactionType
{
    public const string Credit = "credit";
    public const string Debit = "debit";

    public static bool IsValid(string type)
        => type.ToLower() is Credit or Debit;
}