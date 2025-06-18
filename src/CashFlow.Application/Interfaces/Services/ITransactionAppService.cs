using CashFlow.Application.DTOs;

namespace CashFlow.Application.Interfaces.Services;

public interface ITransactionAppService
{
    Task<CreateTransactionRequestDTO> CreateTransactionAsync(CreateTransactionRequestDTO dto);
}