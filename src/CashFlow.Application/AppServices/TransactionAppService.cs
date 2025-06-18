using CashFlow.Application.DTOs;
using CashFlow.Application.Interfaces.Services;
using CashFlow.Domain.Aggregates.CashFlow.Interfaces.Services;


namespace CashFlow.Application.AppServices;

public class TransactionAppService : ITransactionAppService
{
    private readonly ITransactionService _transactionService;
    
    public TransactionAppService(ITransactionService  transactionService)
    {
        _transactionService = transactionService;
    }

    public async Task<CreateTransactionRequestDTO> CreateTransactionAsync(CreateTransactionRequestDTO dto)
    {
        //return await _transactionService.AddAsync(dto);
        return new CreateTransactionRequestDTO();
    }
}