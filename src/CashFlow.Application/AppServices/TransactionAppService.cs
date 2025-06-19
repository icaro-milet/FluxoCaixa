using AutoMapper;
using CashFlow.Application.DTOs;
using CashFlow.Application.Interfaces.Services;
using CashFlow.Domain.Aggregates.CashFlow.Entities;
using CashFlow.Domain.Aggregates.CashFlow.Interfaces.Services;


namespace CashFlow.Application.AppServices;

public class TransactionAppService : ITransactionAppService
{
    private readonly ITransactionService _transactionService;
    private readonly IMapper _mapper;
    public TransactionAppService(
        ITransactionService  transactionService,
        IMapper mapper)
    {
        _transactionService = transactionService;
        _mapper = mapper;
    }

    public async Task<CreateTransactionRequestDTO> CreateTransactionAsync(CreateTransactionRequestDTO dto)
    {
        
        var transaction = _mapper.Map<CreateTransactionRequestDTO, Transaction>(dto);

        var result = await _transactionService.AddAsync(transaction);

        return _mapper.Map<Transaction, CreateTransactionRequestDTO>(result);
    }
}