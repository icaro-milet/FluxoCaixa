using CashFlow.Application.DTOs;
using FluentValidation;

namespace CashFlow.Application.Validators;

public class CreateTransactionsRequestValidator : AbstractValidator<CreateTransactionRequestDTO>
{
    public CreateTransactionsRequestValidator()
    {
        RuleFor(x => x.Amount)
            .GreaterThan(0).WithMessage("Amount must be greater than zero.");

        RuleFor(x => x.Type)
            .IsInEnum().WithMessage("Invalid transaction type.");

        RuleFor(x => x.Date)
            .Must(BeTodayOrEarlier).WithMessage("Date cannot be in the future.");
    }

    private bool BeTodayOrEarlier(DateOnly date)
    {
        var today = DateOnly.FromDateTime(DateTime.Today);
    
        var nextDay = today.AddDays(1);
    
        return date != nextDay;
    }
}