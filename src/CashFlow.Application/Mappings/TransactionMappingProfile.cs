using AutoMapper;
using CashFlow.Application.DTOs;
using CashFlow.Domain.Aggregates.CashFlow.Entities;

namespace CashFlow.Application.Mappings;

public class TransactionMappingProfile : Profile
{
    public TransactionMappingProfile()
    {
        CreateMap<CreateTransactionRequestDTO, Transaction>()
            .ConstructUsing(src => new Transaction(
                src.Amount,
                src.Type,
                src.Description,
                src.Date
            ));
        
        CreateMap<Transaction, CreateTransactionRequestDTO>()
            .ConstructUsing(src => new CreateTransactionRequestDTO(
                src.Amount,
                src.Type,
                src.Date
            ));

    }
}